using System.Collections.Generic;
using Godot;
using GodotBackgroundSimulation.Scripts.BackgroundManagers;
using GodotBackgroundSimulation.Scripts.GameEntities;
using GodotBackgroundSimulation.Scripts.Map;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

public partial class Main : Node
{
    [Export]
    public WorldSketcher WorldSketcher { get; set; }
    
    [Export]
    public PlayerController PlayerController { get; set; }
    
    private WorldManager _worldManager;

    private static readonly Vector2I ChunkSize = new(20, 20);
    private const int LoadRadiusInChunks = 1; // 1 chunk in each direction (total 3x3 chunks)
    private readonly Dictionary<Vector2I, MapChunk> _loadedChunks = [];

    public override void _Ready()
    {
        if (WorldSketcher == null)
        {
            GD.PrintErr("Main: WorldSketcher not found");
            return;
        }
        
        _worldManager = new WorldManager();
        _worldManager.GenerateNewWorld(200, 200, 2000);

        SubscribeToEvents();
    }
    
    private static Vector2I WorldGridToChunkGridPosition(Vector2I worldGridPosition)
    {
        var chunkX = Mathf.FloorToInt((float)worldGridPosition.X / ChunkSize.X);
        var chunkY = Mathf.FloorToInt((float)worldGridPosition.Y / ChunkSize.Y);
        return new Vector2I(chunkX, chunkY);
    }
    
    private void LoadChunk(Vector2I chunkGridPosition)
    {
        var chunkOrigin = chunkGridPosition * ChunkSize;
        var newChunk = new MapChunk(_worldManager.WorldMap, chunkOrigin, ChunkSize);
        _loadedChunks[chunkGridPosition] = newChunk;
        WorldSketcher.DrawMap(newChunk.GetMapCells());
        WorldSketcher.DrawEntities(newChunk.GetEntities());
    }

    private void UnloadChunk(Vector2I chunkGridPosition)
    {
        var chunkToUnload = _loadedChunks[chunkGridPosition];
        WorldSketcher.ClearMap(chunkToUnload.GetMapCells());
        WorldSketcher.ClearEntities(chunkToUnload.GetEntities());
        _loadedChunks.Remove(chunkGridPosition);
    }

    public override void _Process(double delta)
    {
        HandleChunkLoading();
    }

    private void HandleChunkLoading()
    {
        var playerGridPosition = (Vector2I)PlayerController.Position / WorldSketcher.TileMapLayer.TileSet.TileSize;
        var playerChunkGridPosition = WorldGridToChunkGridPosition(playerGridPosition);
        
        var neededChunks = new HashSet<Vector2I>();
        for (var offsetX = -LoadRadiusInChunks; offsetX <= LoadRadiusInChunks; offsetX++)
        {
            for (var offsetY = -LoadRadiusInChunks; offsetY <= LoadRadiusInChunks; offsetY++)
            {
                neededChunks.Add(new Vector2I(playerChunkGridPosition.X + offsetX, playerChunkGridPosition.Y + offsetY));
            }
        }

        foreach (var chunkGridPosition in neededChunks)
        {
            if (!_loadedChunks.ContainsKey(chunkGridPosition))
                LoadChunk(chunkGridPosition);
        }
        
        var chunksToUnload = new List<Vector2I>();
        foreach (var loadedChunk in _loadedChunks.Keys)
        {
            if (!neededChunks.Contains(loadedChunk))
                chunksToUnload.Add(loadedChunk);
        }
        foreach (var chunkGridPosition in chunksToUnload)
        {
            UnloadChunk(chunkGridPosition);
        }
    }
    
    private List<GameEntity> GetEntitiesInLoadedChunks()
    {
        var entities = new List<GameEntity>();
        foreach (var chunk in _loadedChunks.Values)
        {
            entities.AddRange(chunk.GetEntities());
        }
        
        return entities;
    }
    
    private void OnExitTree()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        CalendarManager.OnHourAdvanced += OnHourAdvanced;
        CalendarManager.OnDayAdvanced += OnDayAdvanced;
        CalendarManager.OnSeasonAdvanced += OnSeasonAdvanced;
        CalendarManager.OnYearAdvanced += OnYearAdvanced;
    }
    
    private void UnsubscribeFromEvents()
    {
        CalendarManager.OnHourAdvanced -= OnHourAdvanced;
        CalendarManager.OnDayAdvanced -= OnDayAdvanced;
        CalendarManager.OnSeasonAdvanced -= OnSeasonAdvanced;
        CalendarManager.OnYearAdvanced -= OnYearAdvanced;
    }

    private void OnHourAdvanced()
    {
        WorldSketcher.RefreshEntityVisuals(GetEntitiesInLoadedChunks()); 
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