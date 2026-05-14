namespace MyRaylibProject.Utilities;

public static class Timers
{
    // Here we can change the value based on the deltatime until the interval is reached. Then it will start again.
    public static bool FixedTimer(ref float elapsed, float intervalInSeconds, float deltaTime)
    {
        elapsed += deltaTime;
        if (elapsed < intervalInSeconds)
            return false;
        elapsed = 0;
        return true;
    }

    // Here we can convert the normal from 0 to 1, to 0 to 1 then back to 0
    public static float NormalToUpDown(float normal)
    {
        if (normal < 0.5f)
            return Math.Min(1, normal * 2f);

        return Math.Max(0, 1 - (normal - 0.5f) * 2);
    }


    // This will normalize the time to 0 to 1. Which is easier for calculations.
    public static float TimerNormal(float elapsed, float total)
    {
        return Math.Clamp((elapsed + float.Epsilon) / total, 0, 1);
    }

    // This will return the "frame" for our animation based on the timer.
    public static int TimerStepValue(float normal, int maxSteps)
    {
        return Math.Clamp((int)(normal * maxSteps), 0, maxSteps - 1);
    }
}