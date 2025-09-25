using GodotBackgroundSimulation.Scripts.Enums;

namespace GodotBackgroundSimulation.Scripts.GameEntities.ResourceProviders;

public class ResourceProviderData
{
    public string Id { get; set; }
    public GameEntityTypes Type { get; set; }
    public int CurrentHealth { get; set; }
    public float CurrentGrowth { get; set; }
}