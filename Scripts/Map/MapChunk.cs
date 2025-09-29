using System.Collections.Generic;
using System.Linq;
using Godot;
using GodotBackgroundSimulation.Scripts.GameEntities;

namespace GodotBackgroundSimulation.Scripts.Map;

/// <summary>
/// Represents a chunk of the world map, containing a subset of map cells and entities.
/// </summary>
public class MapChunk()
{
    public Vector2I ChunkWorldPosition { get; private set; }
    
    private readonly MapCell[,] _mapCells;
    private List<GameEntity> _entities = [];
    private readonly Vector2I _chunkSize;
    
    /// <summary>
    /// Creates a new MapChunk at the specified top-left position in the world map.
    /// The chunk will be populated with map cells and entities from the world map.
    /// </summary>
    /// <param name="worldMap"></param>
    /// <param name="chunkTopLeftPosition"></param>
    /// <param name="chunkSize"></param>
    public MapChunk(WorldMap worldMap, Vector2I chunkTopLeftPosition, Vector2I chunkSize) : this()
    {
        _chunkSize = chunkSize;
        ChunkWorldPosition = chunkTopLeftPosition;
        _mapCells = new MapCell[chunkSize.X, chunkSize.Y];
        
        LoadChunk(worldMap);
    }
    
    private void LoadChunk(WorldMap worldMap)
    {
        for (var x = 0; x < _chunkSize.X; x++)
        {
            for (var y = 0; y < _chunkSize.Y; y++)
            {
                var worldX = ChunkWorldPosition.X + x;
                var worldY = ChunkWorldPosition.Y + y;
                
                if (worldX < worldMap.Width && worldY < worldMap.Height && worldX >= 0 && worldY >= 0)
                {
                    _mapCells[x, y] = worldMap.MapCells[worldX, worldY];
                }
            }
        }

        foreach (var entity in worldMap.Entities.Where(entity => IsPositionInChunk(entity.Position)))
        {
            _entities.Add(entity);
        }
    }
    
    public List<GameEntity> GetEntities()
    {
        return _entities;
    }
    
    public MapCell[,] GetMapCells()
    {
        return _mapCells;
    }
    
    private bool IsPositionInChunk(Vector2 position)
    {
        return position.X >= ChunkWorldPosition.X && position.X < (ChunkWorldPosition.X + _chunkSize.X) &&
               position.Y >= ChunkWorldPosition.Y && position.Y < (ChunkWorldPosition.Y + _chunkSize.Y);
    }

    public bool IsPositionAtOrBeyondLeftEdge(Vector2I position)
    {
        return position.X <= ChunkWorldPosition.X;
    }
    
    public bool IsPositionAtOrBeyondRightEdge(Vector2I position)
    {
        return position.X >= (ChunkWorldPosition.X + _chunkSize.X - 1);
    }
    
    public bool IsPositionAtOrAboveTopEdge(Vector2I position)
    {
        return position.Y <= ChunkWorldPosition.Y;
    }

    public bool IsPositionAtOrBelowBottomEdge(Vector2I position)
    {
        return position.Y >= (ChunkWorldPosition.Y + _chunkSize.Y - 1);
    }
}