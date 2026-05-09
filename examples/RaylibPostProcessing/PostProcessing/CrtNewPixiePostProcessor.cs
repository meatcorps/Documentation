using System.Numerics;
using Raylib_cs;

namespace PostProcessingExample.PostProcessing;

public class CrtNewPixiePostProcessor : BaseShader
{
    private static Texture2D? _fallback1x1;
    private Texture2D? _frameTex;
    private float _time;

    public CrtNewPixiePostProcessor()
        : base("Assets/Shaders/crt_newpixie.fx",
        [
            "resolution", "time", "curvature", "wiggleToggle", "scanroll", "vignette", "ghosting"
        ])
    {
    }

    public float Curvature { get; set; } = 2f;
    public float WiggleToggle { get; set; } = 0.0f;
    public float Scanroll { get; set; } = 1.5f;
    public float Vignette { get; set; } = 1.01f;
    public float Ghosting { get; set; } = 0.5f;

    public CrtNewPixiePostProcessor SetFrameTexture(Texture2D tex)
    {
        _frameTex = tex;
        return this;
    }

    protected override void ApplyValues(Shader shader, Texture2D target)
    {
        _time += Raylib.GetFrameTime();

        SetResolutionValue("resolution", target);
        SetValue("time", _time);
        SetValue("curvature", Curvature);
        SetValue("wiggleToggle", WiggleToggle);
        SetValue("scanroll", Scanroll);
        SetValue("vignette", Vignette);
        SetValue("ghosting", Ghosting);
    }

    protected override void DoOverlayRender(int width, int height)
    {
        if (_frameTex is not null)
            Raylib.DrawTexturePro(
                _frameTex.Value,
                new Rectangle(0, 0, _frameTex.Value.Width, _frameTex.Value.Height),
                new Rectangle(0, 0, width, height), Vector2.Zero, 0, Color.White);
    }
}