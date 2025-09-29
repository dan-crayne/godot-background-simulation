using System.Collections.Generic;
using Godot;

namespace GodotBackgroundSimulation.Scripts.GodotScripts.GameEntities;

public partial class GameEntityNodePool<T>(string entityScenePath, int numberToPreload = 100) : Node
    where T : GameEntityVisual
{
    protected readonly Queue<T> Pool = new ();
    protected string EntityScenePath { get; private set; } = entityScenePath;
    protected int NumberToPreload { get; private set; } = numberToPreload;
    
    public int Count => Pool.Count;
    
    public override void _Ready()
    {
        ExpandPool(NumberToPreload);
    }

    public T Get()
    {
        if (Pool.Count > 0)
        {
            var entity = Pool.Dequeue();
            entity.Hide();
            return entity;
        }
        else
        {
            var entityScene = GD.Load<PackedScene>(EntityScenePath);
            var entity = entityScene.Instantiate<T>();
            AddChild(entity);
            return entity;
        }
    }
    
    public void Release(T entity)
    {
        entity.Visible = false;
        Pool.Enqueue(entity);
    }
    
    public void ReleaseAll(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            Release(entity);
        }
    }
    
    public void ExpandPool(int additionalCount)
    {
        var scene = GD.Load<PackedScene>(EntityScenePath);
        for (var i = 0; i < additionalCount; i++)
        {
            var entity = scene.Instantiate<T>();
            entity.Hide();
            AddChild(entity);
            Pool.Enqueue(entity);
        }
    }
}