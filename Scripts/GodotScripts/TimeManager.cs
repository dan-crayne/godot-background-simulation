using Godot;

namespace GodotBackgroundSimulation.Scripts;

public partial class TimeManager : Node
{
   private double ElapsedTime { get; set; }

   public override void _Ready()
   {
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