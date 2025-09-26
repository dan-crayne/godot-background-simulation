namespace GodotBackgroundSimulation.Scripts.Map.MapCellDefinitions;

public struct NormalGround() : IMapCellDefinition
{
   public MapCellTypes Type { get; } = MapCellTypes.GrowthGround;
   public float DefaultMoisture { get; } = 0.25f;
   public float DefaultNutrients { get; } = 1.0f;
}