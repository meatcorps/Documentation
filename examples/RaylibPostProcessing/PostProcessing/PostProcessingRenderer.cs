using Raylib_cs;

namespace PostProcessingExample.PostProcessing;

public sealed class PostProcessingRenderer : IDisposable
{
    private RenderTexture2D? _renderTarget1;
    private RenderTexture2D? _renderTarget2;
    private bool _swapped;

    private RenderTexture2D FromTexture => _swapped ? _renderTarget2!.Value : _renderTarget1!.Value;

    private RenderTexture2D ToTexture => _swapped ? _renderTarget1!.Value : _renderTarget2!.Value;

    public RenderTexture2D Render(IEnumerable<BaseShader> postProcessors, RenderTexture2D sourceTexture)
    {
        var totalEnabled = 0;
        
        foreach (var postProcessor in postProcessors)
            if (postProcessor.Enabled) 
                totalEnabled++;

        if (totalEnabled == 0)
            return sourceTexture;

        _swapped = false;

        _renderTarget1 = CreateRenderTexture(_renderTarget1, sourceTexture.Texture);
        _renderTarget2 = CreateRenderTexture(_renderTarget2, sourceTexture.Texture);

        var first = true;

        foreach (var postProcessor in postProcessors)
        {
            if (!postProcessor.Enabled)
                continue;

            if (postProcessor is INeedsCurrentViewTexture needsSceneTexture)
                needsSceneTexture.SetCurrentViewTexture(sourceTexture.Texture);

            postProcessor.Apply(first ? sourceTexture.Texture : FromTexture.Texture, ToTexture);

            _swapped = !_swapped;
            first = false;
        }

        return FromTexture;
    }

    private RenderTexture2D CreateRenderTexture(RenderTexture2D? target, Texture2D sourceTexture)
    {
        var targetChanged = false;

        if (target is null)
        {
            targetChanged = true;
            target = Raylib.LoadRenderTexture(sourceTexture.Width, sourceTexture.Height);
        }

        if (target.Value.Texture.Width != sourceTexture.Width || target.Value.Texture.Height != sourceTexture.Height)
        {
            targetChanged = true;
            Raylib.UnloadRenderTexture(target.Value);
            target = Raylib.LoadRenderTexture(sourceTexture.Width, sourceTexture.Height);
        }

        if (targetChanged) Raylib.SetTextureFilter(target.Value.Texture, TextureFilter.Point);

        return target.Value;
    }
    
    public void Dispose()
    {
        if (_renderTarget1 is not null)
            Raylib.UnloadRenderTexture(_renderTarget1.Value);
        if (_renderTarget2 is not null)
            Raylib.UnloadRenderTexture(_renderTarget2.Value);
    }
}