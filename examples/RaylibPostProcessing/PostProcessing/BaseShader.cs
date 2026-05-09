using System.Numerics;
using Raylib_cs;

namespace PostProcessingExample.PostProcessing;

public abstract class BaseShader : IDisposable
{
    private Shader _shader;
    private readonly string _fxFilename;
    protected readonly Dictionary<string, int> ShaderLocations = new();
    private bool _isLoaded;

    protected BaseShader(string fxFilename, string[] shaderValues, bool enabled = true)
    {
        Enabled = enabled;
        _fxFilename = fxFilename;
        foreach (var shaderValue in shaderValues) ShaderLocations.Add(shaderValue, 0);
    }

    public bool Enabled { get; set; }

    private bool _isDisposed;
    private bool _shaderLocationsLoaded;

    public void Initialize()
    {
        if (_isLoaded)
            return;

        _isLoaded = true;

        _shader = Raylib.LoadShader(null, _fxFilename);
    }

    public void Apply(Texture2D source, RenderTexture2D target)
    {
        if (!_shaderLocationsLoaded)
        {
            foreach (var shaderLocation in ShaderLocations)
                ShaderLocations[shaderLocation.Key] = Raylib.GetShaderLocation(_shader, shaderLocation.Key);

            _shaderLocationsLoaded = true;
        }

        Raylib.BeginTextureMode(target);
        Raylib.BeginShaderMode(_shader);
        ApplyValues(_shader, source);
        Raylib.DrawTexturePro(
            source,
            new Rectangle(0, 0, target.Texture.Width, -target.Texture.Height), // flip Y
            new Rectangle(0, 0, target.Texture.Width, target.Texture.Height), // upscale for final resolution
            Vector2.Zero,
            0,
            Color.White
        );
        Raylib.EndShaderMode();
        DoOverlayRender(target.Texture.Width, target.Texture.Height);

        Raylib.EndTextureMode();
    }

    protected virtual void DoOverlayRender(int width, int height)
    {
    }

    protected virtual void ApplyValues(Shader shader, Texture2D source)
    {
    }

    private bool TryLoc(string name, out int loc)
    {
        if (!ShaderLocations.TryGetValue(name, out loc)) return false;
        if (loc < 0) return false;
        return true;
    }

    protected void SetValue(string name, float[] value)
    {
        if (!TryLoc(name, out var loc)) return;
        Raylib.SetShaderValue(_shader, loc, value, ShaderUniformDataType.Float);
    }

    protected unsafe void SetValue(string name, float value)
    {
        if (!TryLoc(name, out var loc)) return;

        var buffer = stackalloc float[1];
        buffer[0] = value;

        Raylib.SetShaderValue(_shader, loc, buffer, ShaderUniformDataType.Float);
    }

    protected unsafe void SetValue(string name, int value)
    {
        if (!TryLoc(name, out var loc)) return;

        var buffer = stackalloc int[1];
        buffer[0] = value;

        Raylib.SetShaderValue(_shader, loc, buffer, ShaderUniformDataType.Int);
    }

    protected void SetValue(string name, Vector2 value)
    {
        if (!TryLoc(name, out var loc)) return;
        Raylib.SetShaderValue(_shader, loc, value, ShaderUniformDataType.Vec2);
    }

    protected void SetValue(string name, Vector3 value)
    {
        if (!TryLoc(name, out var loc)) return;
        Raylib.SetShaderValue(_shader, loc, value, ShaderUniformDataType.Vec3);
    }

    protected void SetValue(string name, Color color)
    {
        if (!TryLoc(name, out _)) return;
        // Convert Raylib color (0–255) to normalized RGB (0–1)
        var rgb = new Vector3(color.R / 255f, color.G / 255f, color.B / 255f);
        Raylib.SetShaderValue(_shader, ShaderLocations[name], rgb, ShaderUniformDataType.Vec3);
    }

    protected void SetValue(string name, Color color, bool includeAlpha)
    {
        if (includeAlpha)
        {
            // RGBA (vec4)
            var rgba = new[]
            {
                color.R / 255f,
                color.G / 255f,
                color.B / 255f,
                color.A / 255f
            };
            Raylib.SetShaderValue(_shader, ShaderLocations[name], rgba, ShaderUniformDataType.Vec4);
        }
        else
        {
            // Default to RGB (vec3)
            SetValue(name, color);
        }
    }

    protected void SetResolutionValue(string name, Texture2D value)
    {
        if (!TryLoc(name, out var loc)) return;
        Raylib.SetShaderValue(_shader, loc, new Vector2(value.Width, value.Height), ShaderUniformDataType.Vec2);
    }

    protected Vector2 GetResolution(Texture2D tex)
    {
        return new Vector2(tex.Width, tex.Height);
    }

    public void Dispose()
    {
        if (_isDisposed) 
            return;
        Raylib.UnloadShader(_shader);
        OnDispose();
        _isDisposed = true;
    }

    protected virtual void OnDispose()
    {

    }
}