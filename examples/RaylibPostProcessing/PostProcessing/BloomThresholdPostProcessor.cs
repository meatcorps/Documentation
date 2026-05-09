using Raylib_cs;

namespace PostProcessingExample.PostProcessing;

public class BloomThresholdPostProcessor : BaseShader
{
    public BloomThresholdPostProcessor()
        : base("Assets/Shaders/bloom_threshold.fx", new[] { "threshold", "knee" })
    {
    }

    public float Threshold { get; set; } = 0.8f;
    public float Knee { get; set; } = 0.1f;

    protected override void ApplyValues(Shader shader, Texture2D target)
    {
        SetValue("threshold", Threshold);
        SetValue("knee", Knee);
    }
}