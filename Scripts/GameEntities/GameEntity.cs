using Godot;
using GodotBackgroundSimulation.Scripts.Constants;
using GodotBackgroundSimulation.Scripts.Enums;

namespace GodotBackgroundSimulation.Scripts.GameEntities;

public class GameEntity(GameEntityId id, Vector2I gridPosition, float updateInterval = GameTimeIntervals.Day)
{
    public GameEntityId Id = id;
    public Vector2I GridPosition = gridPosition;
    public float UpdateInterval = updateInterval;
    public GameEntityTypes EntityType = GameEntityTypes.Default;

    public GameEntity() : this(new GameEntityId(), new Vector2I(), GameTimeIntervals.Day)
    {
    }

    public virtual string GetScenePath()
    {
        return "res://Scenes/Placeholders/Placeholder.tscn";
    }
}