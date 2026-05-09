using Raylib_cs;

namespace PostProcessingExample.PostProcessing;

public interface INeedsCurrentViewTexture
{
    
    void SetCurrentViewTexture(Texture2D scene);
}