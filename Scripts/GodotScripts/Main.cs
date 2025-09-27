using Godot;
using GodotBackgroundSimulation.Scripts.BackgroundManagers;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

public partial class Main : Node
{
    [Export]
    public Node2D WorldContainer { get; set; }
    
    private WorldManager _worldManager;
    private WorldSketcher _worldSketcher;
    private Node2D _entityContainer;

    public override void _Ready()
    {
        _worldSketcher = GetNodeOrNull<WorldSketcher>("WorldSketcher");
        if (_worldSketcher == null)
        {
            GD.PrintErr("Main: WorldSketcher not found");
        }
        
        _worldManager = new WorldManager();
        _worldManager.GenerateNewWorld(150, 225);
        
        // set the initial entity container
        _entityContainer = WorldContainer.GetNode<Node2D>("EntityContainer");
        
        RefreshWorldDrawing();
        SubscribeToEvents();
    }
    
    private void SubscribeToEvents()
    {
        CalendarManager.OnHourAdvanced += OnHourAdvanced;
        CalendarManager.OnDayAdvanced += OnDayAdvanced;
        CalendarManager.OnSeasonAdvanced += OnSeasonAdvanced;
        CalendarManager.OnYearAdvanced += OnYearAdvanced;
    }
    
    private void RefreshWorldDrawing()
    {
        ClearEntityContainer();
        CallDeferred("RecreateEntityContainer");
    }

    private void ClearEntityContainer()
    {
        _entityContainer.QueueFree();
    }
    
    private void RecreateEntityContainer()
    {
        _entityContainer = new Node2D();
        WorldContainer.AddChild(_entityContainer);
        
        _worldSketcher?.DrawMap(_worldManager.WorldMap.MapCells);
        _worldSketcher?.DrawEntities(_entityContainer, _worldManager.WorldMap.Entities);
    }
    
    private void OnHourAdvanced()
    {
        GD.Print("OnHourAdvanced - Main");
        RefreshWorldDrawing();
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