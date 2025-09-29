using Godot;
using GodotBackgroundSimulation.Scripts.BackgroundManagers;
using GodotBackgroundSimulation.Scripts.Map;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

public partial class Main : Node
{
    [Export]
    public WorldSketcher WorldSketcher { get; set; }
    
    [Export]
    public bool DrawChunksInsteadOfEverything { get; set; } = true;
    
    private WorldManager _worldManager;
    private MapSuperChunk _mapSuperChunk;

    public override void _Ready()
    {
        if (WorldSketcher == null)
        {
            GD.PrintErr("Main: WorldSketcher not found");
            return;
        }
        
        _worldManager = new WorldManager();
        _worldManager.GenerateNewWorld(200, 200, 2000);

        if (DrawChunksInsteadOfEverything)
            DrawMapSuperChunk(chunkSize: new Vector2I(10, 10), chunkOrigin: new Vector2I(10, 10));
        else
            DrawWorld();
        
        SubscribeToEvents();
    }

    private void DrawMapSuperChunk(Vector2I chunkSize, Vector2I chunkOrigin)
    {
       _mapSuperChunk = new MapSuperChunk(_worldManager.WorldMap, chunkSize, chunkOrigin);
       WorldSketcher.DrawMap(_mapSuperChunk.GetMapCellsForSuperChunk());
       WorldSketcher.DrawEntities(_mapSuperChunk.GetAllEntitiesInSuperChunk());
    }
    
    private void DrawWorld()
    {
        if (_worldManager?.WorldMap == null)
        {
            GD.PrintErr("Main: WorldManager or WorldMap is null");
            return;
        }
        
        WorldSketcher.DrawMap(_worldManager.WorldMap.MapCells);
        WorldSketcher.DrawEntities(_worldManager.WorldMap.Entities);
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