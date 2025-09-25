using System.Collections.Generic;
using Godot;
using GodotBackgroundSimulation.Scripts.GameEntities;
using GodotBackgroundSimulation.Scripts.GameEntities.Map;
using GodotBackgroundSimulation.Scripts.GodotScripts;
using godotbackgroundsimulation.Scripts.Interfaces;

namespace GodotBackgroundSimulation.Scripts.BackgroundManagers;

public class WorldManager
{
   private const int MaxHotEntities = 2000; // if needed for array
   private const int DefaultWorldSizeInMapCells = 500;
   public MapCell[,] MapCells;
   // TODO: determine if this should be split into separate collections for hot/cold entities (array for hot)
   public List<GameEntity> WorldEntities;
   // public GameEntity [] FrequentlyUpdatedWorldEntities =  new GameEntity[MaxHotEntities];

   public WorldManager()
   {
      SubscribeToEvents();
      MapCells = new MapCell[DefaultWorldSizeInMapCells, DefaultWorldSizeInMapCells];
      WorldEntities = new List<GameEntity>();
   }
   
   public WorldManager(MapCell[,] mapCells, List<GameEntity> worldEntities)
   {
      SubscribeToEvents();
      MapCells = mapCells;

      // Fill up entities with blanks for testing
      for (int i = 0; i < 3000; i++)
      {
         WorldEntities.Add(new GameEntity());
      }
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
      foreach (var worldEntity in WorldEntities)
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
      foreach (var worldEntity in WorldEntities)
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