using System.Numerics;
using PostProcessingExample.PostProcessing;

namespace PostProcessingExample.Utilities;

public static class SetupShaders
{
    public static IEnumerable<BaseShader> SetupProcessingBloom(
        float threshold = 0.8f,
        float knee = 0.1f,
        float intensity = 0.6f,
        float spread = 1.0f)
    {
        yield return new BloomThresholdPostProcessor
        {
            Threshold = threshold,
            Knee = knee
        };

        yield return new GaussianBlurPostProcessor
        {
            Direction = new Vector2(1, 0),
            Spread = spread
        };

        yield return new GaussianBlurPostProcessor
            {
                Direction = new Vector2(0, 1),
                Spread = spread
            };

        yield return new BloomCompositePostProcessor
        {
            Intensity = intensity
        };
    }
}