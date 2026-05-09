using System.Numerics;
using Raylib_cs;

namespace PostProcessingExample.PostProcessing;

public class GaussianBlurPostProcessor : BaseShader
{
    public GaussianBlurPostProcessor()
        : base("Assets/Shaders/gaussian_blur.fx", new[] { "resolution", "direction", "spread" })
    {
    }

    public Vector2 Direction { get; set; } = new(1f, 0f); // set (0,1) for vertical
    public float Spread { get; set; } = 1.0f;

    protected override void ApplyValues(Shader shader, Texture2D target)
    {
        SetResolutionValue("resolution", target);
        SetValue("direction", Direction);
        SetValue("spread", Spread);
    }
}