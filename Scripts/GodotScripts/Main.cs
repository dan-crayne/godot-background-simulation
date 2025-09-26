using Godot;
using GodotBackgroundSimulation.Scripts.BackgroundManagers;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

public partial class Main : Node
{
    private WorldManager _worldManager;
    private WorldSketcher _worldSketcher;

    public override void _Ready()
    {
        _worldSketcher = GetNodeOrNull<WorldSketcher>("WorldSketcher");
        if (_worldSketcher == null)
        {
            GD.PrintErr("Main: WorldSketcher not found");
        }
        
        _worldManager = new WorldManager();
        _worldManager.GenerateNewWorld(150, 225);
        
        _worldSketcher?.DrawMap(_worldManager.WorldMap.MapCells);
        _worldSketcher?.DrawEntities(_worldManager.WorldMap.Entities);
    }
}