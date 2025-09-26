using Godot;
using GodotBackgroundSimulation.Scripts.GodotScripts;
using GodotBackgroundSimulation.Scripts.Interfaces;
using GodotBackgroundSimulation.Scripts.Map;

namespace GodotBackgroundSimulation.Scripts.BackgroundManagers;

public class WorldManager
{
   public WorldMap WorldMap { get; private set; }
   
   // TODO: determine if this should be split into separate collections for hot/cold entities (array for hot)
   // private const int MaxHotEntities = 2000; // if needed for array
   // public GameEntity [] FrequentlyUpdatedWorldEntities =  new GameEntity[MaxHotEntities];

   public WorldManager()
   {
      SubscribeToEvents();
   }
   
   public void GenerateNewWorld(int widthInMapCells = 100, int heightInMapCells = 200)
   {
      WorldMap = new WorldMap(widthInMapCells, heightInMapCells);
      WorldMap.GenerateSimpleRandomMap();
   }
   
   private void SubscribeToEvents()
   {
      CalendarManager.OnDayAdvanced += OnDayAdvanced;
      CalendarManager.OnHourAdvanced += OnHourAdvanced;
      CalendarManager.OnSeasonAdvanced += OnSeasonAdvanced;
      CalendarManager.OnYearAdvanced += OnYearAdvanced;
   }

   private void OnHourAdvanced()
   {
      GD.Print("Hour advanced - updating entities");
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
      GD.Print("Day Advanced - updating entities");
      foreach (var worldEntity in WorldMap.Entities)
      {
         if (worldEntity is IDailyUpdatedEntity entity)
         {
            entity.DailyUpdateTasks();
         }
      }
   }

   private void OnSeasonAdvanced()
   {
      GD.Print("Season advanced");
      
   }

   private void OnYearAdvanced()
   {
      GD.Print("Year advanced"); 
   }
}