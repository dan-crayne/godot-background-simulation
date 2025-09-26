namespace GodotBackgroundSimulation.Scripts.GameEntities.ResourceProviders;

public class ResourceProviderTemplate
{
    public string Name { get; set; }
    public ResourceProviderTypes Type { get; set; }
    public GrowthSchedule GrowthSchedule { get; set; }
    public string ScenePath { get; set; }
}