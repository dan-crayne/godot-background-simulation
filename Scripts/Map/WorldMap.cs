using System.Collections.Generic;
using GodotBackgroundSimulation.Scripts.GameEntities;

namespace GodotBackgroundSimulation.Scripts.Map;

public class WorldMap(int width, int height)
{
    public int Width { get; set; } = width;
    public int Height { get; set; } = height;
    public MapCell[,] MapCells { get; set; } = new MapCell[width, height];
    public List<GameEntity> Entities { get; set; } = [];

    public void GenerateSimpleRandomMap(int totalEntities)
    {
        var mapGenerator = new MapGenerator(this, totalEntities);
        mapGenerator.GenerateSimpleMap();
    }
}