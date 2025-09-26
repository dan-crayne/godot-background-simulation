using Godot;

namespace GodotBackgroundSimulation.Scripts.GodotScripts.GameEntities;

public partial class Tree(int growthStage, string id) : Node
{
    private int GrowthStage { get; } = growthStage;
    private string Id { get; } = id;
}
