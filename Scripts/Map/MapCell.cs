using Godot;

namespace GodotBackgroundSimulation.Scripts.Map;

public class MapCell(Vector2 position, MapCellTypes type, float moisture, float nutrients)
{
    public readonly Vector2 Position = position;
    public MapCellTypes Type = type;
    public float Moisture = moisture;
    public float Nutrients = nutrients;
}