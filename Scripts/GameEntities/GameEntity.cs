using Godot;
using GodotBackgroundSimulation.Scripts.Constants;

namespace GodotBackgroundSimulation.Scripts.GameEntities;

public class GameEntity(GameEntityId id, Vector2 position, float updateInterval = GameTimeIntervals.Day)
{
    public GameEntityId Id = id;
    public Vector2 Position = position;
    public float UpdateInterval = updateInterval;

    public GameEntity() : this(new GameEntityId(), new Vector2(), GameTimeIntervals.Day)
    {
    }

    public virtual string GetScenePath()
    {
        return "res://Scenes/Placeholders/Placeholder.tscn";
    }
}