using System.Collections.Generic;
using Godot;
using GodotBackgroundSimulation.Scripts.Enums;
using GodotBackgroundSimulation.Scripts.GameEntities;
using GodotBackgroundSimulation.Scripts.GodotScripts.GameEntities;
using GodotBackgroundSimulation.Scripts.Map;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

public partial class WorldSketcher : Node2D
{
   [Export]
   public TileMapLayer TileMapLayer { get; set; }
   [Export]
   public Vector2I NormalGroundAtlasPosition { get; set; }
   
   [Export]
   public TreeGameEntityPool TreeGameEntityPool { get; set; }

   private List<TreeVisual> ActiveTreeVisuals { get; set; } = [];
   
   public void DrawMap(MapCell[,] mapCells)
   {
      for (int x = 0; x < mapCells.GetLength(0); x++)
      {
         for (int y = 0; y < mapCells.GetLength(1); y++)
         {
            if (mapCells[x, y] != null)
            {
               var mapCellPositionX = mapCells[x, y].Position.X;
               var mapCellPositionY = mapCells[x, y].Position.Y;
               TileMapLayer.SetCell(new Vector2I(mapCellPositionX, mapCellPositionY), 0, NormalGroundAtlasPosition); 
            }
         }
      }
   }
   
   public void ClearMap(MapCell[,] mapCells)
   {
      for (int x = 0; x < mapCells.GetLength(0); x++)
      {
         for (int y = 0; y < mapCells.GetLength(1); y++)
         {
            if (mapCells[x, y] != null)
            {
               var mapCellPositionX = mapCells[x, y].Position.X;
               var mapCellPositionY = mapCells[x, y].Position.Y;
               TileMapLayer.SetCell(new Vector2I(mapCellPositionX, mapCellPositionY), -1); 
            }
         }
      }
   }
   
   public void DrawEntities(List<GameEntity> entities)
   {
      foreach (var entity in entities)
      {
         if (entity.EntityType == GameEntityTypes.ResourceProvider)
         {
            var treeVisual = TreeGameEntityPool.Get();
            treeVisual.Position = entity.GridPosition * TileMapLayer.TileSet.TileSize;
            treeVisual.GameEntityId = entity.Id;
            treeVisual.Show();
            ActiveTreeVisuals.Add(treeVisual);
         }
      }
   }

   public void ClearEntities(List<GameEntity> entities)
   {
      // TODO: this should clear all entities, not just trees
      foreach (var entity in entities)
      {
         if (entity.EntityType == GameEntityTypes.ResourceProvider)
         {
            var treeVisual = ActiveTreeVisuals.Find(tv => tv.GameEntityId == entity.Id);
            if (treeVisual != null)
            {
               ActiveTreeVisuals.Remove(treeVisual);
               TreeGameEntityPool.Release(treeVisual);
            }
            else
            {
               GD.PrintErr($"Tree visual for entity ID {entity.Id} not found in active visuals");
            }
         }
      }
   }
   
   public void RefreshEntityVisuals(List<GameEntity> entities)
   {
      foreach (var treeVisual in ActiveTreeVisuals)
      {
         var entity = entities.Find(e => e.Id == treeVisual.GameEntityId);
         if (entity == null)
         {
            GD.PrintErr($"Entity with ID {treeVisual.GameEntityId} not found");
            continue;
         }
         
         treeVisual.SyncVisualWithBackendEntity(entity);
      }
   }
}