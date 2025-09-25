namespace GodotBackgroundSimulation.Scripts.Constants;

public static class GameTimeIntervals
{
    public const float Year = Season * 4;
    public const float Season = Day * 28;
    public const float Day = Hour * 24f;
    public const float Hour = 1.0f;
}