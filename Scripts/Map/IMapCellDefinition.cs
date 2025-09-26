namespace GodotBackgroundSimulation.Scripts.Map;

public interface IMapCellDefinition
{
    public MapCellTypes Type { get; }
    public float DefaultMoisture { get;  }
    public float DefaultNutrients { get; }
}