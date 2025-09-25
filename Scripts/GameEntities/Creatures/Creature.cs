using Godot;

namespace GodotBackgroundSimulation.Scripts.GameEntities;

public class Creature
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public Vector2 TargetPosition { get; set; }
    public int CurrentHealth { get; set; }
}