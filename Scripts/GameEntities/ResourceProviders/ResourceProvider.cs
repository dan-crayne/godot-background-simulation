using GodotBackgroundSimulation.Scripts.Interfaces;
using GodotBackgroundSimulation.Scripts.Constants;
using Godot;

namespace GodotBackgroundSimulation.Scripts.GameEntities.ResourceProviders;

public class ResourceProvider : GameEntity, IHourlyUpdatedEntity
{
    private ResourceProviderTemplate _template;
    private float _currentHealth;
    private float _currentGrowth;

    public ResourceProvider(ResourceProviderTemplate template, Vector2 position) : base(new GameEntityId(), position, GameTimeIntervals.Hour)
    {
        _template = template;
        _currentGrowth = 0;
        _currentHealth = template.GrowthSchedule.GrowthStages[0].HealthAtStage;
        EntityType = Enums.GameEntityTypes.ResourceProvider;
    }
    
    public int GetCurrentGrowthStage()
    {
        var currentStage = 0;
        for (int i = 0; i < _template.GrowthSchedule.GrowthStages.Count; i++)
        {
            if (_currentGrowth >= _template.GrowthSchedule.GrowthStages[i].GrowthRequired)
            {
                currentStage = i;
            }
        }

        return currentStage;
    }
    
    public void HourlyUpdateTasks()
    {
        _currentGrowth += 1;
    }

    public override string GetScenePath()
    {
        return _template.ScenePath;
    }
}