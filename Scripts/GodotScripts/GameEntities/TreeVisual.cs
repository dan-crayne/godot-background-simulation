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
    
    // Note: don't get this node with GetNode() as it will cause a memory leak
    [Export]
    public Sprite2D Sprite2D { get; set; }
    
    public override void SyncVisualWithBackendEntity(GameEntity gameEntity)
    {
        if (gameEntity is ResourceProvider resourceProvider)
        {
            Sprite2D.Frame = resourceProvider.GetCurrentGrowthStage();
        }
        else
        {
            GD.PrintErr("TreeVisual.SyncVisualWithBackendEntity: Provided entity is not a ResourceProvider.");
        }
    }
}
