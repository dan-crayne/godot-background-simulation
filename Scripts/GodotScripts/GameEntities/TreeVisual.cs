using Godot;
using GodotBackgroundSimulation.Scripts.GameEntities;
using GodotBackgroundSimulation.Scripts.GameEntities.ResourceProviders;

namespace GodotBackgroundSimulation.Scripts.GodotScripts.GameEntities;

public partial class TreeVisual : GameEntityVisual
{
    // needed for godot
    public TreeVisual() : base()
    {
    }
    
    public override void SyncVisualWithBackendEntity(GameEntity gameEntity)
    {
        var sprite = GetNode<Sprite2D>("Sprite2D");
        if (gameEntity is ResourceProvider resourceProvider)
        {
            sprite.Frame = resourceProvider.GetCurrentGrowthStage();
        }
        else
        {
            GD.PrintErr("TreeVisual.SyncVisualWithBackendEntity: Provided entity is not a ResourceProvider.");
        }
    }
}
