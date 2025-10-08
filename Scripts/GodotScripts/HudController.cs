using Godot;
using GodotBackgroundSimulation.Scripts.GodotScripts;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

public partial class HudController : Node
{
    [Export] public Label DayAndHourValue { get; set; }
    [Export] public Label SeasonAndYearValue { get; set; }
    [Export] public Label PlayerPositionValue { get; set; }
    [Export] public Label PlayerTilemapLayerCellPositionValue { get; set; }
    
    public override void _Process(double delta)
    {
        var currentTime = CalendarManager.Instance.GetCurrentTime();
        if (DayAndHourValue != null)
        {
            DayAndHourValue.Text = $"{currentTime.Hour:D2}:00 {currentTime.Day} Day";
        }
        
        if (SeasonAndYearValue != null)
        {
            SeasonAndYearValue.Text = $"Season {currentTime.Season:D2}, Year {currentTime.Year:D2}";
        }

        if (PlayerPositionValue != null && Main.Instance != null && Main.Instance.PlayerController != null)
        {
            var playerPosition = Main.Instance.PlayerController.Position;
            PlayerPositionValue.Text = $"X:{playerPosition.X:F0} Y:{playerPosition.Y:F0}";
        }
        
        if (PlayerTilemapLayerCellPositionValue != null && Main.Instance != null && Main.Instance.PlayerController != null && Main.Instance.WorldSketcher != null)
        {
            var cellPosition = Main.Instance.GetPlayerWorldCellPosition();
            PlayerTilemapLayerCellPositionValue.Text = $"X:{cellPosition.X} Y:{cellPosition.Y}";
        }
    }
}
