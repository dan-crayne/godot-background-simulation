using System.Collections.Generic;
using System.Numerics;
using Godot;
using GodotBackgroundSimulation.Scripts.BackgroundManagers;
using GodotBackgroundSimulation.Scripts.GameEntities;

namespace GodotBackgroundSimulation.Scripts.Map;

public class MapChunkManager
{
    private Dictionary<Vector2I, MapChunk> _chunkDictionary = new Dictionary<Vector2I, MapChunk>();
    private Vector2I _chunkSize;
    
    public MapChunkManager(WorldMap worldMap, Vector2I chunkSize, Vector2I centralChunkTopLeft)
    {
        _chunkSize = chunkSize;
        PopulateSuperChunk(worldMap, centralChunkTopLeft);
    }

    public void PopulateSuperChunk(WorldMap worldMap, Vector2I centralChunkTopLeft)
    {
        _chunkDictionary.Clear();
        
        for (var chunkVectorX = -1; chunkVectorX <= 1; chunkVectorX++)
        {
            for (var chunkVectorY = -1; chunkVectorY <= 1; chunkVectorY++)
            {
                var chunkOrigin = centralChunkTopLeft +
                                  new Vector2I(chunkVectorX * _chunkSize.X, chunkVectorY * _chunkSize.Y);
                _chunkDictionary.Add(new Vector2I(chunkVectorX, chunkVectorY), new MapChunk(worldMap, chunkOrigin, _chunkSize));
            }
        }
    }
    
    public Vector2I GetChunkFromWorldPosition(Vector2I worldPosition)
    {
        foreach (var pair in _chunkDictionary)
        {
            if (pair.Value.IsPositionInChunk(worldPosition))
            {
                return pair.Key; // Return relative position to central chunk
            }
        }
        
        return Vector2I.MaxValue; // Not found
    }

    public Vector2I GetCentralChunkTopLeft()
    {
        return _chunkDictionary.TryGetValue(Vector2I.Zero, out var chunk)
            ? chunk.ChunkWorldPosition
            : Vector2I.MaxValue;
    }
    
    public MapCell[,] GetMapCellsForSuperChunk()
    {
        // Combine all 9 chunks into a single larger array of map cells
        var combinedWidth = _chunkSize.X * 3;
        var combinedHeight = _chunkSize.Y * 3;
        var combinedCells = new MapCell[combinedWidth, combinedHeight];
        
        foreach (var chunk in _chunkDictionary.Values)
        {
            var chunkCells = chunk.GetMapCells();
            var chunkOrigin = chunk.ChunkWorldPosition - GetCentralChunkTopLeft() + new Vector2I(_chunkSize.X, _chunkSize.Y);
            
            for (var x = 0; x < _chunkSize.X; x++)
            {
                for (var y = 0; y < _chunkSize.Y; y++)
                {
                    var combinedX = chunkOrigin.X + x;
                    var combinedY = chunkOrigin.Y + y;
                    
                    if (combinedX >= 0 && combinedX < combinedWidth && combinedY >= 0 && combinedY < combinedHeight)
                    {
                        combinedCells[combinedX, combinedY] = chunkCells[x, y];
                    }
                }
            }
        }

        return combinedCells;
    }
    
    public List<GameEntity> GetAllEntitiesInSuperChunk()
    {
        var allEntities = new List<GameEntity>();
        
        foreach (var chunk in _chunkDictionary.Values)
        {
            allEntities.AddRange(chunk.GetEntities());
        }

        return allEntities;
    }
    
    public void ShiftLeft(WorldMap worldMap)
    {
        var newDict = new Dictionary<Vector2I, MapChunk>();
        foreach (var pair in _chunkDictionary)
        {
            var newKey = new Vector2I(pair.Key.X - 1, pair.Key.Y);
            if (newKey.X >= -1)
                newDict[newKey] = pair.Value;
        }
        for (int y = -1; y <= 1; y++)
        {
            var newChunkOrigin = _chunkDictionary[new Vector2I(0, y)].ChunkWorldPosition + new Vector2I(_chunkSize.X, 0);
            newDict[new Vector2I(1, y)] = new MapChunk(worldMap, newChunkOrigin, _chunkSize);
        }
        _chunkDictionary = newDict;
    }

    public void ShiftRight(WorldMap worldMap)
    {
        var newDict = new Dictionary<Vector2I, MapChunk>();
        foreach (var pair in _chunkDictionary)
        {
            var newKey = new Vector2I(pair.Key.X + 1, pair.Key.Y);
            if (newKey.X <= 1)
                newDict[newKey] = pair.Value;
        }
        for (int y = -1; y <= 1; y++)
        {
            var newChunkOrigin = _chunkDictionary[new Vector2I(0, y)].ChunkWorldPosition - new Vector2I(_chunkSize.X, 0);
            newDict[new Vector2I(-1, y)] = new MapChunk(worldMap, newChunkOrigin, _chunkSize);
        }
        _chunkDictionary = newDict;
    }

    public void ShiftUp(WorldMap worldMap)
    {
        var newDict = new Dictionary<Vector2I, MapChunk>();
        foreach (var pair in _chunkDictionary)
        {
            var newKey = new Vector2I(pair.Key.X, pair.Key.Y - 1);
            if (newKey.Y >= -1)
                newDict[newKey] = pair.Value;
        }
        for (int x = -1; x <= 1; x++)
        {
            var newChunkOrigin = _chunkDictionary[new Vector2I(x, 0)].ChunkWorldPosition + new Vector2I(0, _chunkSize.Y);
            newDict[new Vector2I(x, 1)] = new MapChunk(worldMap, newChunkOrigin, _chunkSize);
        }
        _chunkDictionary = newDict;
    }

    public void ShiftDown(WorldMap worldMap)
    {
        var newDict = new Dictionary<Vector2I, MapChunk>();
        foreach (var pair in _chunkDictionary)
        {
            var newKey = new Vector2I(pair.Key.X, pair.Key.Y + 1);
            if (newKey.Y <= 1)
                newDict[newKey] = pair.Value;
        }
        for (int x = -1; x <= 1; x++)
        {
            var newChunkOrigin = _chunkDictionary[new Vector2I(x, 0)].ChunkWorldPosition - new Vector2I(0, _chunkSize.Y);
            newDict[new Vector2I(x, -1)] = new MapChunk(worldMap, newChunkOrigin, _chunkSize);
        }
        _chunkDictionary = newDict;
    }
}