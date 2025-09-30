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
    private readonly Vector2I _chunkWorldGridPosition;
    private readonly MapCell[,] _mapCells;
    private readonly List<GameEntity> _entities = [];
    private readonly Vector2I _chunkSize;
    
    /// <summary>
    /// Creates a new MapChunk at the specified top-left position in the world map.
    /// The chunk will be populated with map cells and entities from the world map.
    /// </summary>
    /// <param name="worldMap"></param>
    /// <param name="chunkTopLeftGridPosition"></param>
    /// <param name="chunkSize"></param>
    public MapChunk(WorldMap worldMap, Vector2I chunkTopLeftGridPosition, Vector2I chunkSize) : this()
    {
        _chunkSize = chunkSize;
        _chunkWorldGridPosition = chunkTopLeftGridPosition;
        _mapCells = new MapCell[chunkSize.X, chunkSize.Y];
        
        LoadChunk(worldMap);
    }
    
    private void LoadChunk(WorldMap worldMap)
    {
        for (var x = 0; x < _chunkSize.X; x++)
        {
            for (var y = 0; y < _chunkSize.Y; y++)
            {
                var worldX = _chunkWorldGridPosition.X + x;
                var worldY = _chunkWorldGridPosition.Y + y;
                
                if (worldX < worldMap.Width && worldY < worldMap.Height && worldX >= 0 && worldY >= 0)
                {
                    _mapCells[x, y] = worldMap.MapCells[worldX, worldY];
                }
            }
        }

        foreach (var entity in worldMap.Entities.Where(entity => IsGridPositionInChunk(entity.GridPosition)))
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
    
    private bool IsGridPositionInChunk(Vector2I gridPosition)
    {
        return gridPosition.X >= _chunkWorldGridPosition.X && gridPosition.X < (_chunkWorldGridPosition.X + _chunkSize.X) &&
               gridPosition.Y >= _chunkWorldGridPosition.Y && gridPosition.Y < (_chunkWorldGridPosition.Y + _chunkSize.Y);
    }
}