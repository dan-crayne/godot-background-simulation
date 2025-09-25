using GodotBackgroundSimulation.Scripts.Enums;
using godotbackgroundsimulation.Scripts.Interfaces;

namespace GodotBackgroundSimulation.Scripts.GameEntities.ResourceProviders;

public class ResourceProvider : GameEntity, IHourlyUpdatedEntity
{
    public ResourceProviderTypes Type { get; set; }
    public float CurrentHealth { get; set; }
    public float CurrentGrowth { get; set; }

    public void HourlyUpdateTasks()
    {
        CurrentGrowth += 1;
    }
}