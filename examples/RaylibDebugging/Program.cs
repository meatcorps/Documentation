using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Raylib_cs;
using ZLogger;
using ZLogger.Providers;

namespace HelloWorld;

internal static class Program
{
    private static ILogger _logger;
    
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    private static unsafe void LogCustom(int logLevel, sbyte* text, sbyte* args)
    {
        var message = Logging.GetLogMessage(new IntPtr(text), new IntPtr(args));

        var convertedLogLevel = (TraceLogLevel)logLevel switch
        {
            TraceLogLevel.All => LogLevel.Trace,
            TraceLogLevel.Trace => LogLevel.Trace,
            TraceLogLevel.Debug => LogLevel.Debug,
            TraceLogLevel.Info => LogLevel.Information,
            TraceLogLevel.Warning => LogLevel.Warning,
            TraceLogLevel.Error => LogLevel.Error,
            TraceLogLevel.Fatal => LogLevel.Critical,
            TraceLogLevel.None => LogLevel.None,
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
        };
        
        _logger.Log(convertedLogLevel, message);
    }
    
    [System.STAThread]
    public static void Main()
    {
        
       using var factory = LoggerFactory.Create(logging =>
        {
            // Set the minimum log level
            logging.SetMinimumLevel(LogLevel.Trace);

            // Add the file logger with a rolling interval of 1 day
            logging.AddZLoggerRollingFile(options =>
            {
                // Format the log message
                options.UsePlainTextFormatter(formatter =>
                {
                    formatter.SetPrefixFormatter($"{0:utc-longdate} [{1:long}] ", (in template, in info) => template.Format(info.Timestamp, info.LogLevel));
                });
                
                // Where to write the log file
                options.FilePathSelector = (timestamp, sequenceNumber) =>
                    $"logs/{timestamp.ToLocalTime():yyyy-MM-dd}_{sequenceNumber:000}.log";
                
                // How often to roll the log file
                options.RollingInterval = RollingInterval.Day;
                // How many KB to keep before rolling
                options.RollingSizeKB = 1024;
            });
            
            // Add the logger to the console
            logging.AddZLoggerConsole(options =>
            {
                options.UsePlainTextFormatter(formatter =>
                {
                    // Format the log message with color codes
                    formatter.SetPrefixFormatter(
                        $"{0}{1:local-longdate} [{2:short}] ",
                        (in template, in info) =>
                        {
                            var color = info.LogLevel switch
                            {
                                LogLevel.Trace => "\x1b[90m",       // gray
                                LogLevel.Debug => "\x1b[36m",       // cyan
                                LogLevel.Information => "\x1b[32m", // green
                                LogLevel.Warning => "\x1b[33m",     // yellow
                                LogLevel.Error => "\x1b[31m",       // red
                                LogLevel.Critical => "\x1b[35m",    // magenta
                                _ => "\x1b[0m"
                            };

                            template.Format(color, info.Timestamp, info.LogLevel);
                        });

                    formatter.SetSuffixFormatter(
                        $"{0}",
                        (in template, in info) => template.Format("\x1b[0m"));
                });
            });
        });
        _logger = factory.CreateLogger("Program");

        _logger.LogInformation("Game started");
        
        Raylib.SetTraceLogLevel(TraceLogLevel.All);
        unsafe
        {
            Raylib.SetTraceLogCallback(&LogCustom);
        }
        Raylib.InitWindow(800, 480, "Hello World");
        var texture = Raylib.LoadTexture("info.png");
        _logger.LogInformation("Loaded texture {texture}", texture.Id);
        var gizmos = new Queue<Action>();
        while (!Raylib.WindowShouldClose())
        {
            var start = Stopwatch.GetTimestamp();
            var alloc = GC.GetAllocatedBytesForCurrentThread();
            gizmos.Enqueue(() => Raylib.DrawText("Info 1", 50, 50, 20, Color.Red));
            
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);

            while (gizmos.TryDequeue(out var gizmo))
                gizmo();

            // Just to allocate for testing (128 bytes)
            var test = $"{gizmos.Count} gizmos whatever";
            test += "ALLOC";
            
            Raylib.EndDrawing();
            var totalAlloc = GC.GetAllocatedBytesForCurrentThread() - alloc;
            var totalUs = (Stopwatch.GetTimestamp() - start) * 1_000_000 / Stopwatch.Frequency;
            Raylib.SetWindowTitle($"Total time: {totalUs}us | Alloc: {totalAlloc} bytes");
        }

        Raylib.UnloadTexture(texture);
        Raylib.CloseWindow();
        
        unsafe
        {
            Raylib.SetTraceLogCallback(&Logging.LogConsole);
        }
    }
}