using Godot;
using GodotBackgroundSimulation.Scripts.GodotScripts;
using GodotBackgroundSimulation.Scripts.Interfaces;
using GodotBackgroundSimulation.Scripts.Map;

namespace GodotBackgroundSimulation.Scripts.BackgroundManagers;

public class WorldManager
{
   public WorldMap WorldMap { get; private set; }
   
   public WorldManager()
   {
      SubscribeToEvents();
   }
   
   public void GenerateNewWorld(int widthInMapCells, int heightInMapCells, int totalEntities)
   {
      WorldMap = new WorldMap(widthInMapCells, heightInMapCells);
      WorldMap.GenerateSimpleRandomMap(totalEntities);
   }
   
   private void SubscribeToEvents()
   {
      CalendarManager.OnDayAdvanced += OnDayAdvanced;
      CalendarManager.OnHourAdvanced += OnHourAdvanced;
      CalendarManager.OnSeasonAdvanced += OnSeasonAdvanced;
      CalendarManager.OnYearAdvanced += OnYearAdvanced;
   }
   
   private void UnsubscribeFromEvents()
   {
      CalendarManager.OnDayAdvanced -= OnDayAdvanced;
      CalendarManager.OnHourAdvanced -= OnHourAdvanced;
      CalendarManager.OnSeasonAdvanced -= OnSeasonAdvanced;
      CalendarManager.OnYearAdvanced -= OnYearAdvanced;
   }

   private void OnHourAdvanced()
   {
      // GD.Print("Hour advanced - updating entities");
      foreach (var worldEntity in WorldMap.Entities)
      {
         if (worldEntity is IHourlyUpdatedEntity entity)
         {
            entity.HourlyUpdateTasks();
         }
      }
   }

   private void OnDayAdvanced()
   {
      GD.Print("Day Advanced");
   }

   private void OnSeasonAdvanced()
   {
      GD.Print("Season advanced");
      
   }

   private void OnYearAdvanced()
   {
      GD.Print("Year advanced"); 
   }
   
   ~WorldManager()
   {
      UnsubscribeFromEvents();
   }
}