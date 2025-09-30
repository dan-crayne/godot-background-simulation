using Godot;
using GodotBackgroundSimulation.Scripts.BackgroundManagers;
using GodotBackgroundSimulation.Scripts.Map;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

public partial class Main : Node
{
    [Export]
    public WorldSketcher WorldSketcher { get; set; }
    
    [Export]
    public bool DrawChunksInsteadOfEntireWorld { get; set; } = true;
    
    [Export]
    public PlayerController PlayerController { get; set; }
    
    private WorldManager _worldManager;
    private MapChunkManager _mapChunkManager;
    
    private readonly Vector2I _chunkSize = new Vector2I(10, 10);
    private readonly Vector2I _initialChunkOrigin = new Vector2I(10, 10);

    public override void _Ready()
    {
        if (WorldSketcher == null)
        {
            GD.PrintErr("Main: WorldSketcher not found");
            return;
        }
        
        _worldManager = new WorldManager();
        _worldManager.GenerateNewWorld(200, 200, 2000);

        if (DrawChunksInsteadOfEntireWorld)
        {
            DrawMapSuperChunk(chunkSize: _chunkSize, chunkOrigin: _initialChunkOrigin);
            
        }
        else
            DrawWorld();
        
        SubscribeToEvents();
    }


    public override void _Process(double delta)
    {
        var normalizedPosition = (Vector2I)PlayerController.Position / WorldSketcher.TileMapLayer.TileSet.TileSize;
        
        var chunkDirection = _mapChunkManager.GetChunkFromWorldPosition(normalizedPosition);
        if (chunkDirection == Vector2.Zero)
        {
            GD.Print("Player in central chunk, no shift needed");
        }
        if (chunkDirection == Vector2I.Up)
        {
            GD.Print("Chunk up");
            _mapChunkManager.ShiftUp(_worldManager.WorldMap);
            DrawMapSuperChunk(_chunkSize, _mapChunkManager.GetCentralChunkTopLeft());
        }
        else if (chunkDirection == Vector2I.Down)
        {
            GD.Print("Chunk down");
            _mapChunkManager.ShiftDown(_worldManager.WorldMap);
            DrawMapSuperChunk(_chunkSize, _mapChunkManager.GetCentralChunkTopLeft());
        }
        else if (chunkDirection == Vector2I.Left)
        {
            GD.Print("Chunk left");
            _mapChunkManager.ShiftLeft(_worldManager.WorldMap);
            DrawMapSuperChunk(_chunkSize, _mapChunkManager.GetCentralChunkTopLeft());
        }
        else if (chunkDirection == Vector2I.Right)
        {
            GD.Print("Chunk right");
            _mapChunkManager.ShiftRight(_worldManager.WorldMap);
            DrawMapSuperChunk(_chunkSize, _mapChunkManager.GetCentralChunkTopLeft());
        }
    }

    private void DrawMapSuperChunk(Vector2I chunkSize, Vector2I chunkOrigin)
    {
       _mapChunkManager = new MapChunkManager(_worldManager.WorldMap, chunkSize, chunkOrigin);
       WorldSketcher.DrawMap(_mapChunkManager.GetMapCellsForSuperChunk());
       WorldSketcher.DrawEntities(_mapChunkManager.GetAllEntitiesInSuperChunk());
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
        // GD.Print("OnHourAdvanced - Main");
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