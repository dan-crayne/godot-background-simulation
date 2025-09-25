using Godot;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

public partial class TimeManager : Node
{
   private double ElapsedTime { get; set; }

   public override void _Ready()
   {
      GD.Print("TimeManager");
      ElapsedTime = 0;
   }

   public override void _Process(double deltaTime)
   {
      ElapsedTime += deltaTime;
   }

   public double GetElapsedTime()
   {
      return ElapsedTime;
   }
}