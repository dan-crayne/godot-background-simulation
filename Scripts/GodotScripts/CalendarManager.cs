using System;
using Godot;

namespace GodotBackgroundSimulation.Scripts;

public partial class CalendarManager : Node
{
   public float lengthOfDay = 24.0f;
   public float lengthOfHour = 1.0f;
   public event Action DayAdvanced;

   private TimeManager _timeManager;
   private int _lastHour;

   public override void _Ready()
   {
      var parent = GetParent();
      _timeManager = parent.GetNodeOrNull<TimeManager>("TimeManager");
      if (_timeManager == null)
      {
         GD.PrintErr("Calendar Manager: TimeManager not found");
      }
   }
   public override void _Process(double deltaTime)
   {
      var elapsedTime = _timeManager.GetElapsedTime();
      if (elapsedTime >= lengthOfDay)
      {
         DayAdvanced?.Invoke();
      }
      
   }
}