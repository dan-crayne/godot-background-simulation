using Godot;

namespace GodotBackgroundSimulation.Scripts.Map;

public class MapChunkManager
{
   private MapSuperChunk _mapSuperChunk;
   private Vector2I _chunkSize;

   public MapChunkManager(WorldMap worldMap, Vector2I centralChunkStartingPosition, Vector2I chunkSize)
   {
      // _mapSuperChunk = new MapSuperChunk(worldMap, chunkSize);
      // _chunkSize = chunkSize;
      // _mapSuperChunk.PopulateSuperChunk(centralChunkStartingPosition);
   }
}