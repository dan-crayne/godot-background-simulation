using Godot;

namespace GodotBackgroundSimulation.Scripts.GodotScripts.GameEntities;

public partial class TreeGameEntityPool()
   : GameEntityNodePool<TreeVisual>("res://Scenes/GameEntities/TreeGameEntity.tscn", 500);