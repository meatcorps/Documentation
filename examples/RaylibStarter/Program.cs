using Raylib_cs;

namespace HelloWorld;

internal class Program
{
    public static void Main(string[] args)
    {
        Raylib.InitWindow(800, 480, "Hello World");

        using var texture = new RaylibTexture("Assets/image.png");
        //var texture = Raylib.LoadTexture("Assets/image.png");
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);
            
            Raylib.DrawTexture(texture, 12, 52, Color.White);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
        
        
    }
}

public readonly struct RaylibTexture: IDisposable
{
    public readonly Texture2D Texture;
    
    public RaylibTexture(string path)
    {
        Texture = Raylib.LoadTexture(path);
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