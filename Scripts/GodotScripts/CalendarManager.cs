using System;
using Godot;
using GodotBackgroundSimulation.Scripts.Constants;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

public partial class CalendarManager : Node
{
   public static event Action OnDayAdvanced;
   public static event Action OnHourAdvanced;
   public static event Action OnSeasonAdvanced;
   public static event Action OnYearAdvanced;

   private TimeManager _timeManager;
   private int _previousHour;
   private int _previousDay;
   private int _previousSeason;
   private int _previousYear;

   public override void _Ready()
   {
      GD.Print("CalendarManager");
      var parent = GetParent();
      _timeManager = parent.GetNodeOrNull<TimeManager>("TimeManager");
      if (_timeManager == null)
      {
         GD.PrintErr("Calendar Manager: TimeManager not found");
      }
   }

   public int GetHour()
   {
      return (int)(_timeManager?.GetElapsedTime() / GameTimeIntervals.Hour ?? 0);
   }

   public int GetDay()
   {
      return (int)(_timeManager?.GetElapsedTime() / GameTimeIntervals.Day ?? 0);
   }

   public int GetSeason()
   {
      return (int)(_timeManager?.GetElapsedTime() / GameTimeIntervals.Season ?? 0);
   }

   public int GetYear()
   {
      return (int)(_timeManager?.GetElapsedTime() / GameTimeIntervals.Year ?? 0);
   }
   
   public override void _Process(double deltaTime)
   {
      var currentHour = GetHour();
      if (currentHour > _previousHour)
      {
         GD.Print($"Previous: {_previousHour}; current hour: {currentHour}");
         _previousHour = currentHour;
         OnHourAdvanced?.Invoke();
      }
      var currentDay = GetDay();
      if (currentDay > _previousDay)
      {
         GD.Print($"Previous: {_previousDay}; current hour: {currentDay}");
         _previousDay = currentDay;
         OnDayAdvanced?.Invoke();
      }
      var currentSeason = GetSeason();
      if (currentSeason > _previousSeason)
      {
         _previousSeason = currentSeason;
         OnSeasonAdvanced?.Invoke();
      }

      var currentYear = GetYear();
      if (currentYear > _previousYear)
      {
         _previousYear = currentYear;
         OnYearAdvanced?.Invoke();
      }
   }
}