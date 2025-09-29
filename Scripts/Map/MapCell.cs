using Godot;

namespace GodotBackgroundSimulation.Scripts.Map;

public class MapCell(Vector2I position, MapCellTypes type, float moisture, float nutrients)
{
    public readonly Vector2I Position = position;
    public MapCellTypes Type = type;
    public float Moisture = moisture;
    public float Nutrients = nutrients;
}