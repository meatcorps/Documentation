using Raylib_cs;

namespace PostProcessingExample.PostProcessing;

public class BloomCompositePostProcessor : BaseShader, INeedsCurrentViewTexture
{
    private Texture2D _scene;

    public BloomCompositePostProcessor()
        : base("Assets/Shaders/bloom_composite.fx", new[] { "sceneTex", "intensity" })
    {
    }

    public float Intensity { get; set; } = 0.6f;

    public void SetCurrentViewTexture(Texture2D scene)
    {
        _scene = scene;
    }

    protected override void ApplyValues(Shader shader, Texture2D target)
    {
        // Bind the original scene as a second sampler
        Raylib.SetShaderValueTexture(shader, ShaderLocations["sceneTex"], _scene);
        SetValue("intensity", Intensity);
    }
}