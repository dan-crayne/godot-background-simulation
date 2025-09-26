using System.Collections.Generic;
using Godot;
using GodotBackgroundSimulation.Scripts.GameEntities;
using GodotBackgroundSimulation.Scripts.GameEntities.ResourceProviders;

namespace GodotBackgroundSimulation.Scripts.Map;

public class MapGenerator(WorldMap worldMapInstance, int entitiesToCreate = 2000)
{
    public void GenerateSimpleMap()
    {
        worldMapInstance.Entities = new List<GameEntity>();
        MapCell[,] mapCells = new MapCell[worldMapInstance.Width, worldMapInstance.Height];
        
        // Fill map with random entities
        for (int i = 0; i < entitiesToCreate; i++)
        {
            var randomPosition = new Godot.Vector2(GD.Randi() % worldMapInstance.Width, GD.Randi() % worldMapInstance.Height);
            
            var possibleEntities = new List<GameEntity>()
            {
                new ResourceProvider(ResourceProviderData.ConiferTree, randomPosition)
            };
            
            var entityToAdd = possibleEntities[(int)GD.Randi() % possibleEntities.Count];
            worldMapInstance.Entities.Add(entityToAdd);
        }
        
        // Fill map with default cells
        for (int x = 0; x < worldMapInstance.Width; x++)
        {
            for (int y = 0; y < worldMapInstance.Height; y++)
            {
                mapCells[x, y] = new MapCell(new Godot.Vector2(x, y), MapCellTypes.GrowthGround, 20, 100);
            }
        }
    }
}