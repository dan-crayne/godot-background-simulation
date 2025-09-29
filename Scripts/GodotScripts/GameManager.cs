using Godot;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

[GlobalClass]
public partial class GameManager : Node
{
   private Main _mainNode;
   
   public override void _Ready()
   {
      GD.Print("GameManager");
      _mainNode = GetNode<Main>("../Main");
   }
}
