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
            new GrowthStage { growthRequired = 0, healthAtStage = 50 },
            new GrowthStage { growthRequired = 100, healthAtStage = 100 },
            new GrowthStage { growthRequired = 300, healthAtStage = 200 },
            new GrowthStage { growthRequired = 600, healthAtStage = 300 }
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
            new GrowthStage { growthRequired = 0, healthAtStage = 40 },
            new GrowthStage { growthRequired = 120, healthAtStage = 90 },
            new GrowthStage { growthRequired = 350, healthAtStage = 220 },
            new GrowthStage { growthRequired = 700, healthAtStage = 320 }
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
            new GrowthStage { growthRequired = 0, healthAtStage = 30 },
            new GrowthStage { growthRequired = 50, healthAtStage = 80 },
            new GrowthStage { growthRequired = 150, healthAtStage = 150 }
         }
      }
   };
}
