using System.Numerics;
using System.Runtime.CompilerServices;
using MyRaylibProject.Utilities;
using Raylib_cs;

namespace MyRaylibProject;

internal static class Program
{
    public static readonly Dictionary<Sprites, SpriteInfo> SpriteSet = new();
    
    [System.STAThread]
    public static void Main()
    {
        Raylib.InitWindow(800, 480, "Hello World");
        
        const int GRID_SIZE = 16;
        
        using var spriteAtlas = new RaylibTexture("Assets/Sprites.png", TextureFilter.Point);
        SpriteSet[Sprites.Robot] = new SpriteInfo(spriteAtlas, 0, 0, 1, 1, GRID_SIZE);
        SpriteSet[Sprites.Gate] = new SpriteInfo(spriteAtlas, 0, 3, 1, 1, GRID_SIZE, 6);
        SpriteSet[Sprites.Floor] = new SpriteInfo(spriteAtlas, 0, 4, 1, 1, GRID_SIZE);
        SpriteSet[Sprites.Wall] = new SpriteInfo(spriteAtlas, 3, 5, 1, 1, GRID_SIZE);

        var gateTime = 0f;
        var gateDuration = 1f;
        
        var rotationTime = 0f;
        var rotationDuration = 1f;
        
        var halfGridSize = new Vector2(GRID_SIZE / 2, GRID_SIZE / 2);
        
        var camera = new Camera2D();
        camera.Zoom = 2;
        camera.Offset = new Vector2(GRID_SIZE, GRID_SIZE);
        
        var windowSize = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()) / camera.Zoom;
        
        var patchInfo = new NPatchInfo
        {
             Layout = NPatchLayout.NinePatch,
             Bottom = 1,
             Left = 1,
             Right = 1,
             Top = 1,
             Source = SpriteSet[Sprites.Floor].Rectangle,
        };
        
        while (!Raylib.WindowShouldClose())
        {
            var deltaTime = Raylib.GetFrameTime();
            Timers.FixedTimer(ref gateTime, gateDuration, deltaTime);
            var normalizedTime = Timers.TimerNormal(gateTime, gateDuration);
            var normalizedUpDown = Timers.NormalToUpDown(normalizedTime);
            var gateFrame = Timers.TimerStepValue(normalizedUpDown, SpriteSet[Sprites.Gate].TotalFrames);
            
            Timers.FixedTimer(ref rotationTime, rotationDuration, deltaTime);
            
            var rotation = Timers.TimerNormal(rotationTime, rotationDuration) * 360;
            
            Raylib.BeginDrawing();
            Raylib.BeginMode2D(camera);
            Raylib.ClearBackground(Color.Black);
            
            for (var x = 0; x < windowSize.X / GRID_SIZE; x++)
            {
                for (var y = 0; y < windowSize.Y / GRID_SIZE; y++)
                {
                    var position = new Vector2(x, y) * GRID_SIZE;
                    SpriteSet[Sprites.Wall].Draw(position, Color.White);
                }
            }
            
            //SpriteSet[Sprites.Gate].Draw(new Vector2(GRID_SIZE * 2, GRID_SIZE * 2), Color.White, gateFrame);

            //SpriteSet[Sprites.Robot].Draw(new Vector2(GRID_SIZE * 4, GRID_SIZE * 2), Color.White, rotation: rotation);

            Raylib.BeginBlendMode(BlendMode.Multiplied);
            SpriteSet[Sprites.Robot].Draw(new Vector2(GRID_SIZE * 6, GRID_SIZE * 2), Color.White);
            Raylib.EndBlendMode();
            
            Raylib.BeginBlendMode(BlendMode.Additive);
            SpriteSet[Sprites.Robot].Draw(new Vector2(GRID_SIZE * 8, GRID_SIZE * 2), Color.White);
            Raylib.EndBlendMode();
            
            Raylib.BeginBlendMode(BlendMode.AddColors);
            SpriteSet[Sprites.Robot].Draw(new Vector2(GRID_SIZE * 10, GRID_SIZE * 2), Color.White);
            Raylib.EndBlendMode();
            
            Raylib.BeginBlendMode(BlendMode.Alpha);
            SpriteSet[Sprites.Robot].Draw(new Vector2(GRID_SIZE * 12, GRID_SIZE * 2), Color.White);
            Raylib.EndBlendMode();
            
            Raylib.BeginBlendMode(BlendMode.AlphaPremultiply);
            SpriteSet[Sprites.Robot].Draw(new Vector2(GRID_SIZE * 14, GRID_SIZE * 2), Color.White);
            Raylib.EndBlendMode();
            
            Raylib.BeginBlendMode(BlendMode.Multiplied);
            SpriteSet[Sprites.Robot].Draw(new Vector2(GRID_SIZE * 16, GRID_SIZE * 2), Color.White);
            Raylib.EndBlendMode();
            
            Raylib.BeginBlendMode(BlendMode.SubtractColors);
            SpriteSet[Sprites.Robot].Draw(new Vector2(GRID_SIZE * 18, GRID_SIZE * 2), Color.White);
            Raylib.EndBlendMode();
            
            //SpriteSet[Sprites.Robot].Draw(new Vector2(GRID_SIZE * 22, GRID_SIZE * 2), Color.White, scale: 4);
            
            Raylib.DrawTextureNPatch(
                spriteAtlas, 
                patchInfo, 
                new Rectangle(halfGridSize + new Vector2(64, 64), 
                    new Vector2(windowSize.X - 128, 64)), 
                Vector2.Zero, 0, Color.White);
            
            Raylib.EndMode2D();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}

public enum Sprites
{
    Robot,
    Gate,
    Floor,
    Wall
}

public readonly struct RaylibTexture : IDisposable
{
    public readonly Texture2D Texture;

    public RaylibTexture(string path, TextureFilter filter = TextureFilter.Point)
    {
        Texture = Raylib.LoadTexture(path);
        Raylib.SetTextureFilter(Texture, filter);
    }

    public static implicit operator Texture2D(RaylibTexture texture)
    {
        return texture.Texture;
    }

    public void Dispose()
    {
        Raylib.UnloadTexture(Texture);
    }
}

public readonly struct SpriteInfo
{
    public readonly RaylibTexture Texture;
    public readonly Rectangle Rectangle;
    public readonly int TotalFrames;
    
    public SpriteInfo(RaylibTexture texture, Rectangle rectangle, int totalFrames = 1)
    {
        Texture = texture;
        Rectangle = rectangle;
        TotalFrames = totalFrames;
    }
    
    public SpriteInfo(RaylibTexture texture, int x, int y, int width, int height, int gridSize = 1, int totalFrames = 1)
    {
        Texture = texture;
        Rectangle = new Rectangle(x * gridSize, y * gridSize, width * gridSize, height * gridSize);
        TotalFrames = totalFrames;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Draw(Vector2 position, Color color, int frame = 0, float scale = 1, float rotation = 0)
    {
        Raylib.DrawTexturePro(
            Texture, 
            new Rectangle(Rectangle.X + frame * Rectangle.Width, Rectangle.Y, Rectangle.Width, Rectangle.Height), 
            new Rectangle(position.X, position.Y, Rectangle.Width * scale, Rectangle.Height * scale), 
            new Vector2(Rectangle.Width / 2, Rectangle.Height / 2) * scale, 
            rotation, 
            color);
    }
    
}