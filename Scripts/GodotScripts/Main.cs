using Godot;
using GodotBackgroundSimulation.Scripts.BackgroundManagers;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

public partial class Main : Node
{
    [Export]
    public WorldSketcher WorldSketcher { get; set; }
    
    private WorldManager _worldManager;

    public override void _Ready()
    {
        // WorldSketcher = GetNodeOrNull<WorldSketcher>("WorldSketcher");
        if (WorldSketcher == null)
        {
            GD.PrintErr("Main: WorldSketcher not found");
            return;
        }
        
        _worldManager = new WorldManager();
        _worldManager.GenerateNewWorld(200, 200, 2000);
        
        WorldSketcher.DrawMap(_worldManager.WorldMap.MapCells);
        WorldSketcher.DrawEntities(_worldManager.WorldMap.Entities);
        
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        CalendarManager.OnHourAdvanced += OnHourAdvanced;
        CalendarManager.OnDayAdvanced += OnDayAdvanced;
        CalendarManager.OnSeasonAdvanced += OnSeasonAdvanced;
        CalendarManager.OnYearAdvanced += OnYearAdvanced;
    }

    private void OnHourAdvanced()
    {
        GD.Print("OnHourAdvanced - Main");
        WorldSketcher.RefreshEntityVisuals(_worldManager.WorldMap.Entities); 
    }
    
    private void OnDayAdvanced()
    {
        
    }
    
    private void OnSeasonAdvanced()
    {
    }
    
    private void OnYearAdvanced()
    {
    }
}