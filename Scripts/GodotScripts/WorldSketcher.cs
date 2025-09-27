using System.Collections.Generic;
using Godot;
using GodotBackgroundSimulation.Scripts.Enums;
using GodotBackgroundSimulation.Scripts.GameEntities;
using GodotBackgroundSimulation.Scripts.GameEntities.ResourceProviders;
using GodotBackgroundSimulation.Scripts.GodotScripts.GameEntities;
using GodotBackgroundSimulation.Scripts.Map;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

public partial class WorldSketcher : Node
{
   [Export]
   public TileMapLayer TileMapLayer { get; set; }
   
   [Export]
   public Vector2I NormalGroundAtlasPosition { get; set; } = new Vector2I(0, 0);
   
   [Export]
   public Node2D WorldNode { get; set; }
   
   public override void _Ready()
   {
      GD.Print("WorldSketcher ready");
   }

   public void DrawMap(MapCell[,] mapCells)
   {
      for (int x = 0; x < mapCells.GetLength(0); x++)
      {
         for (int y = 0; y < mapCells.GetLength(1); y++)
         {
           TileMapLayer.SetCell(new Vector2I(x, y), 0, NormalGroundAtlasPosition); 
         }
      }
   }
   
   public void DrawEntities(Node entityContainer, List<GameEntity> entities)
   {
      foreach (GameEntity entity in entities)
      {
         var scene = GD.Load<PackedScene>(entity.GetScenePath());
         var instance = scene.Instantiate();
         if (entity.EntityType == GameEntityTypes.ResourceProvider)
         {
            if (instance is TreeGameEntity tree)
            {
               tree.Position = entity.Position * TileMapLayer.TileSet.TileSize;
               tree.GrowthStage = (entity as ResourceProvider).GetCurrentGrowthStage();
            }
            entityContainer.AddChild(instance);
         }
         else if (instance is Node2D node2d)
         {
            node2d.Position = entity.Position * TileMapLayer.TileSet.TileSize;
            entityContainer.AddChild(instance);
         }
         
      }
   }
}