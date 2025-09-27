using Godot;

namespace GodotBackgroundSimulation.Scripts.GodotScripts.GameEntities;

public partial class TreeGameEntity(int growthStage, string id) : Node2D
{
    public int GrowthStage { get; set; } = growthStage;
    public string Id { get; set; } = id;
    
    public override void _Ready()
    {
        UpdateSprite();
    }
    
    private void UpdateSprite()
    {
        var sprite = GetNode<Sprite2D>("Sprite2D");
        sprite.Frame = GrowthStage;
    }
}
