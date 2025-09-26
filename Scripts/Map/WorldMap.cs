using System.Collections.Generic;
using GodotBackgroundSimulation.Scripts.GameEntities;

namespace GodotBackgroundSimulation.Scripts.Map;

public class WorldMap
{
    public int Width { get; set; }
    public int Height { get; set; }
    public MapCell[,] MapCells { get; set; }
    public List<GameEntity> Entities { get; set; } = new List<GameEntity>();
    
    public WorldMap(int width, int height)
    {
        Width = width;
        Height = height;
        MapCells = new MapCell[width, height];
    }

    public void GenerateSimpleRandomMap(int entities = 2000)
    {
        var mapGenerator = new MapGenerator(this);
        mapGenerator.GenerateSimpleMap();
    }
}