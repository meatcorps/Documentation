using System.Numerics;
using PostProcessingExample.PostProcessing;
using PostProcessingExample.Utilities;
using Raylib_cs;

namespace PostProcessingExample;

internal static class Program
{
    [System.STAThread]
    public static void Main()
    {
        // Small remark. In my previous tutorials I mention the loading and unloading of textures trick.
        // I did not apply it here. Because I wanted to keep this example as barebones as possible.
        // This makes it easier to use in your own projects. You're welcome ;)
        
        Raylib.InitWindow(800, 480, "Hello World");
        
        // Post-processing renderer it's main task is to ping-pong between the render textures and give the final render texture.
        using PostProcessingRenderer postProcessingRenderer = new();
        
        // Render texture where we will render everything. So, we can apply post-processing effects to it.
        var renderLayer = Raylib.LoadRenderTexture(800, 480);
        
        // Texture for the CRT effect
        var frameTexture = Raylib.LoadTexture("Assets/CRTSidePanels.png");
        
        // Example texture for testing
        var imageTexture = Raylib.LoadTexture("Assets/Image.png");
        
        // Load shaders
        var shaders = new List<BaseShader>();
        shaders.AddRange(SetupShaders.SetupProcessingBloom());
        shaders.Add(new CrtNewPixiePostProcessor()
            .SetFrameTexture(frameTexture));

        // Initialize shaders. By loading the actual shader files, this will also compile the shader.
        foreach (var shader in shaders)
            shader.Initialize();
        
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            
            // Start the render texture. Here is the actual "drawing".
            Raylib.BeginTextureMode(renderLayer);
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.White);
            Raylib.DrawTexture(imageTexture, 12, 32, Color.White);
            
            // Here we end the "drawing"
            Raylib.EndTextureMode();

            // Make the drawing results shiny :D. By applying some post-processing effects!
            var finalTexture = postProcessingRenderer.Render(shaders, renderLayer);
            
            // Render the post-processing results!
            Raylib.DrawTexturePro(
                finalTexture.Texture,
                new Rectangle(0, 0, finalTexture.Texture.Width, -finalTexture.Texture.Height), 
                new Rectangle(0, 0, finalTexture.Texture.Width, finalTexture.Texture.Height), 
                Vector2.Zero,
                0,
                Color.White
            );
            
            Raylib.EndDrawing();
        }

        // Unload everything! We don't want to leak memory!
        foreach (var shader in shaders)
            shader.Dispose();
        Raylib.UnloadRenderTexture(renderLayer);
        Raylib.UnloadTexture(frameTexture);
        Raylib.UnloadTexture(imageTexture);
        Raylib.CloseWindow();
    }
}