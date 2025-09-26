using System.Collections.Generic;
using Godot;
using GodotBackgroundSimulation.Scripts.GameEntities;
using GodotBackgroundSimulation.Scripts.Interfaces;
using GodotBackgroundSimulation.Scripts.Map;

namespace GodotBackgroundSimulation.Scripts.GodotScripts;

public partial class WorldSketcher : Node
{
   [Export]
   public TileMapLayer TileMapLayer { get; set; }
   
   [Export]
   public Vector2I NormalGroundAtlasPosition { get; set; } = new Vector2I(0, 0);
   
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
   
   public void DrawEntities(List<GameEntity> entities)
   {
      foreach (GameEntity entity in entities)
      {
         var scene = GD.Load<PackedScene>(entity.GetScenePath());
         var instance = scene.Instantiate<Node2D>();
         var parent = GetParent();
         parent.AddChild(instance);
         // TODO: use map_to_world to get the correct position (also cell_size / 2 to center it)
         instance.Position = entity.Position * TileMapLayer.TileSet.TileSize;
      }
   }
}