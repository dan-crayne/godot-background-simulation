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
   private int _lastHour;
   private int _lastDay;
   private int _lastSeason;
   private int _lastYear;

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
      return (int)(_timeManager?.GetElapsedTime() % GameTimeIntervals.Day ?? 0);
   }

   public int GetDay()
   {
      return (int)(_timeManager?.GetElapsedTime() % GameTimeIntervals.Season ?? 0);
   }

   public int GetSeason()
   {
      return (int)(_timeManager?.GetElapsedTime() % GameTimeIntervals.Year ?? 0);
   }

   public int GetYear()
   {
      return (int)(_timeManager?.GetElapsedTime() / GameTimeIntervals.Year ?? 0);
   }
   
   public override void _Process(double deltaTime)
   {
      var currentHour = GetHour();
      if (currentHour > _lastHour)
      {
         _lastHour = currentHour + 1;
         OnHourAdvanced?.Invoke();
      }
      var currentDay = GetDay();
      if (currentDay > _lastDay)
      {
         _lastDay = currentDay + 1;
         OnDayAdvanced?.Invoke();
      }
      var currentSeason = GetSeason();
      if (currentSeason > _lastSeason)
      {
         _lastSeason = currentSeason + 1;
         OnSeasonAdvanced?.Invoke();
      }

      var currentYear = GetYear();
      if (currentYear > _lastYear)
      {
         _lastYear = currentYear;
         OnYearAdvanced?.Invoke();
      }
   }
}