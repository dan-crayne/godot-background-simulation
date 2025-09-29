using System.Collections.Generic;
using Godot;
using GodotBackgroundSimulation.Scripts.BackgroundManagers;
using GodotBackgroundSimulation.Scripts.GameEntities;

namespace GodotBackgroundSimulation.Scripts.Map;

public class MapSuperChunk
{
    private MapChunk[,] _chunks = new MapChunk[3, 3];
    private Vector2I _chunkSize;
    
    public MapSuperChunk(WorldMap worldMap, Vector2I chunkSize, Vector2I centralChunkTopLeft)
    {
        _chunkSize = chunkSize;
        PopulateSuperChunk(worldMap, centralChunkTopLeft);
    }

    public void PopulateSuperChunk(WorldMap worldMap, Vector2I centralChunkTopLeft)
    {
        /* Populate the grid of chunks around the central chunk (chunk 4 in this diagram)
           +---+---+---+
           | 0 | 1 | 2 |
           +---+---+---+
           | 3 | 4 | 5 |
           +---+---+---+
           | 6 | 7 | 8 |
           +---+---+---+
        */

        // Loop through the 3x3 grid of chunks (this could be extended to larger grids if needed)
        // We calculate the top-left position of each chunk based on the central chunk's top-left position
        // We will iterate from -1 to 1 for both x and y to cover all 9 chunks, starting with the top-left chunk and
        // working top to bottom, left to right.

        // For each chunk, we calculate its top-left position by adding the appropriate multiple of the chunk size
        // to the central chunk's top-left position. We then create a new MapChunk instance for each position and store it in the _chunks array.

        for (var chunkXMultiplier = -1; chunkXMultiplier <= 1; chunkXMultiplier++)
        {
            for (var chunkYMultiplier = -1; chunkYMultiplier <= 1; chunkYMultiplier++)
            {
                var chunkOrigin = centralChunkTopLeft +
                                  new Vector2I(chunkXMultiplier * _chunkSize.X, chunkYMultiplier * _chunkSize.Y);
                _chunks[chunkXMultiplier + 1, chunkYMultiplier + 1] = new MapChunk(worldMap, chunkOrigin, _chunkSize);
            }
        }
    }

    public Vector2I GetCentralChunkTopLeft()
    {
        return _chunks[1, 1].ChunkWorldPosition;
    }
    
    public MapCell[,] GetMapCellsForSuperChunk()
    {
        // Combine all 9 chunks into a single larger array of map cells
        var combinedWidth = _chunkSize.X * 3;
        var combinedHeight = _chunkSize.Y * 3;
        var combinedCells = new MapCell[combinedWidth, combinedHeight];
        
        foreach (var chunk in _chunks)
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
        
        foreach (var chunk in _chunks)
        {
            allEntities.AddRange(chunk.GetEntities());
        }

        return allEntities;
    }

    public void ShiftLeft(WorldMap worldMap)
    {
        for (var y = 0; y < 3; y++)
        {
            var newChunkOrigin = _chunks[1, y].ChunkWorldPosition + new Vector2I(_chunkSize.X, 0);
            _chunks[0, y] = _chunks[1, y];
            _chunks[1, y] = _chunks[2, y];
            _chunks[2, y] = new MapChunk(worldMap, newChunkOrigin / _chunkSize, _chunkSize);
        }
    }
    
    public void ShiftRight(WorldMap worldMap)
    {
        for (var y = 0; y < 3; y++)
        {
            var newChunkOrigin = _chunks[1, y].ChunkWorldPosition - new Vector2I(_chunkSize.X, 0);
            _chunks[2, y] = _chunks[1, y];
            _chunks[1, y] = _chunks[0, y];
            _chunks[0, y] = new MapChunk(worldMap, newChunkOrigin / _chunkSize, _chunkSize);
        }
    }
    
    public void ShiftUp(WorldMap worldMap)
    {
        for (var x = 0; x < 3; x++)
        {
            var newChunkOrigin = _chunks[x, 1].ChunkWorldPosition + new Vector2I(0, _chunkSize.Y);
            _chunks[x, 0] = _chunks[x, 1];
            _chunks[x, 1] = _chunks[x, 2];
            _chunks[x, 2] = new MapChunk(worldMap, newChunkOrigin / _chunkSize, _chunkSize);
        }
    }
    
    public void ShiftDown(WorldMap worldMap)
    {
        for (var x = 0; x < 3; x++)
        {
            var newChunkOrigin = _chunks[x, 1].ChunkWorldPosition - new Vector2I(0, _chunkSize.Y);
            _chunks[x, 2] = _chunks[x, 1];
            _chunks[x, 1] = _chunks[x, 0];
            _chunks[x, 0] = new MapChunk(worldMap, newChunkOrigin / _chunkSize, _chunkSize);
        }
    }
}