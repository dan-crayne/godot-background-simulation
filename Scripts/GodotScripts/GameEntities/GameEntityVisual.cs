using Godot;
using System;
using GodotBackgroundSimulation.Scripts.GameEntities;

namespace GodotBackgroundSimulation.Scripts.GodotScripts.GameEntities;

public partial class GameEntityVisual(GameEntityId gameEntityId) : Node2D
{
    public GameEntityId GameEntityId { get; set; } = gameEntityId;

    protected GameEntityVisual() : this(new GameEntityId())
    {
    }
    
    public virtual void SyncVisualWithBackendEntity(GameEntity gameEntity)
    {
    }
}
