using System.Collections.Generic;
using Godot;
using GodotBackgroundSimulation.Scripts.GameEntities;

namespace GodotBackgroundSimulation.Scripts.Map;

/// <summary>
/// Represents a chunk of the world map, containing a subset of map cells and entities.
/// </summary>
public class MapChunk()
{
    private MapCell[,] _mapCells;
    private List<GameEntity>  _entities;
    private Vector2I _chunkSize;
    private Vector2I _chunkWorldPosition;
    private WorldMap _worldMap;
    
    /// <summary>
    /// Creates a new MapChunk at the specified top-left position in the world map.
    /// The chunk will be populated with map cells and entities from the world map.
    /// </summary>
    /// <param name="worldMap"></param>
    /// <param name="chunkTopLeftPosition"></param>
    /// <param name="chunkSize"></param>
    public MapChunk(WorldMap worldMap, Vector2I chunkTopLeftPosition, Vector2I chunkSize) : this()
    {
        _worldMap = worldMap;
        _chunkSize = chunkSize;
        _chunkWorldPosition = chunkTopLeftPosition * chunkSize;
        _mapCells = new MapCell[chunkSize.X, chunkSize.Y];
        
        LoadChunk();
    }
    
    private void LoadChunk()
    {
        for (int x = 0; x < _chunkSize.X; x++)
        {
            for (int y = 0; y < _chunkSize.Y; y++)
            {
                int worldX = _chunkWorldPosition.X + x;
                int worldY = _chunkWorldPosition.Y + y;
                
                if (worldX < _worldMap.Width && worldY < _worldMap.Height)
                {
                    _mapCells[x, y] = _worldMap.MapCells[worldX, worldY];
                }
            }
        }
        
        foreach (var entity in _worldMap.Entities)
        {
            if (IsPositionInChunk(entity.Position))
            {
                _entities.Add(entity);
            }
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
        return position.X >= _chunkWorldPosition.X && position.X < (_chunkWorldPosition.X + _chunkSize.X) &&
               position.Y >= _chunkWorldPosition.Y && position.Y < (_chunkWorldPosition.Y + _chunkSize.Y);
    }

    public bool IsPositionAtOrBeyondLeftEdge(Vector2I position)
    {
        return position.X <= _chunkWorldPosition.X;
    }
    
    public bool IsPositionAtOrBeyondRightEdge(Vector2I position)
    {
        return position.X >= (_chunkWorldPosition.X + _chunkSize.X - 1);
    }
    
    public bool IsPositionAtOrAboveTopEdge(Vector2I position)
    {
        return position.Y <= _chunkWorldPosition.Y;
    }

    public bool IsPositionAtOrBelowBottomEdge(Vector2I position)
    {
        return position.Y >= (_chunkWorldPosition.Y + _chunkSize.Y - 1);
    }
}