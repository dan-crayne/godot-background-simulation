using Godot;
using GodotBackgroundSimulation.Scripts.BackgroundManagers;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

public partial class Main : Node
{
    private WorldManager _worldManager;

    public override void _Ready()
    {
        _worldManager = new WorldManager();
    }
}