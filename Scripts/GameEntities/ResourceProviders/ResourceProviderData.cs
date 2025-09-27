using System.Collections.Generic;

namespace GodotBackgroundSimulation.Scripts.GameEntities.ResourceProviders;

public static class ResourceProviderData
{
   public static ResourceProviderTemplate ConiferTree = new ResourceProviderTemplate
   {
      Name = "Conifer Tree",
      Type = ResourceProviderTypes.Tree,
      GrowthSchedule = new GrowthSchedule
      {
         GrowthStages = new List<GrowthStage>
         {
            new GrowthStage { GrowthRequired = 0, HealthAtStage = 50 },
            new GrowthStage { GrowthRequired = 3, HealthAtStage = 100 },
            new GrowthStage { GrowthRequired = 4, HealthAtStage = 200 },
            new GrowthStage { GrowthRequired = 5, HealthAtStage = 300 }
         }
      },
      ScenePath = "res://Scenes/Tree.tscn"
   };
   
   public static ResourceProviderTemplate DesiduousTree = new ResourceProviderTemplate
   {
      Name = "Deciduous Tree",
      Type = ResourceProviderTypes.Tree,
      GrowthSchedule = new GrowthSchedule
      {
         GrowthStages = new List<GrowthStage>
         {
            new GrowthStage { GrowthRequired = 0, HealthAtStage = 40 },
            new GrowthStage { GrowthRequired = 120, HealthAtStage = 90 },
            new GrowthStage { GrowthRequired = 350, HealthAtStage = 220 },
            new GrowthStage { GrowthRequired = 700, HealthAtStage = 320 }
         }
      },
      ScenePath = "res://Scenes/Tree.tscn"
   };
   
   public static ResourceProviderTemplate BerryBush = new ResourceProviderTemplate
   {
      Name = "Berry Bush",
      Type = ResourceProviderTypes.Bush,
      GrowthSchedule = new GrowthSchedule
      {
         GrowthStages = new List<GrowthStage>
         {
            new GrowthStage { GrowthRequired = 0, HealthAtStage = 30 },
            new GrowthStage { GrowthRequired = 50, HealthAtStage = 80 },
            new GrowthStage { GrowthRequired = 150, HealthAtStage = 150 }
         }
      }
   };
}
