using Godot;
using GodotBackgroundSimulation.Scripts.BackgroundManagers;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

public partial class Main : Node
{
    [Export]
    public Node2D WorldContainer { get; set; }
    
    private WorldManager _worldManager;
    private WorldSketcher _worldSketcher;

    public override void _Ready()
    {
        _worldSketcher = GetNodeOrNull<WorldSketcher>("WorldSketcher");
        if (_worldSketcher == null)
        {
            GD.PrintErr("Main: WorldSketcher not found");
            return;
        }
        
        _worldManager = new WorldManager();
        _worldManager.GenerateNewWorld(50, 50, 200);
        
        _worldSketcher.DrawMap(_worldManager.WorldMap.MapCells);
        _worldSketcher.DrawEntities(_worldManager.WorldMap.Entities);
        
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
        _worldSketcher.RefreshEntityVisuals(_worldManager.WorldMap.Entities); 
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