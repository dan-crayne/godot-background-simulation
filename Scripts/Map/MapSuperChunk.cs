using Godot;

namespace GodotBackgroundSimulation.Scripts.Map;

public class MapSuperChunk
{
    private MapChunk[,] _chunks;
    private WorldMap _worldMap;
    private Vector2I _superChunkCenterChunkOrigin;
    private Vector2I _chunkSize;

    public MapSuperChunk(WorldMap worldMap, Vector2I superChunkCenterChunkOrigin, Vector2I chunkSize)
    {
        _worldMap = worldMap;
        _superChunkCenterChunkOrigin = superChunkCenterChunkOrigin;
        _chunkSize = chunkSize;
        // Initialize a 3x3 grid of chunks
        _chunks = new MapChunk[3, 3];
        var middleChunkIndex = new Vector2I(1, 1);

        PopulateSuperChunk();
    }

    private void PopulateSuperChunk()
    {
        /* Populate the 3x3 grid of chunks around the center chunk
           +---+---+---+
           | 0 | 1 | 2 |
           +---+---+---+
           | 3 | 4 | 5 |
           +---+---+---+
           | 6 | 7 | 8 |
           +---+---+---+
           Where chunk 4 is the center chunk at _superChunkCenterChunkOrigin
        */ 
        
        for (int chunkX = -1; chunkX <= 1; chunkX++)
        {
            for (int chunkY = -1; chunkY <= 1; chunkY++)
            {
                var chunkOrigin = _superChunkCenterChunkOrigin + new Vector2I(chunkX, chunkY);
                _chunks[chunkX + 1, chunkY + 1] = new MapChunk(_worldMap, chunkOrigin, _chunkSize);
            }
        }
    }
}