namespace GodotBackgroundSimulation.Scripts.Constants;

public static class GameTimeIntervals
{
    public const float Year = Season * 4; // 4 seasons in a year
    public const float Season = Day * 28; // 28 days in a season
    public const float Day = Hour * 24; // 24 hours in a day
    public const float Hour = 1.0f; // seconds per hour
}