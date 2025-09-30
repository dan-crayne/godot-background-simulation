using System.Collections.Generic;
using Godot;

namespace GodotBackgroundSimulation.Scripts.GodotScripts.GameEntities;

public partial class GameEntityNodePool<T>(string entityScenePath, int numberToPreload) : Node2D
    where T : GameEntityVisual
{
    private readonly Queue<T> _pool = new ();
    private string _entityScenePath = entityScenePath;
    private int _numberToPreload = numberToPreload;
    
    private int Count => _pool.Count;
    
    public override void _Ready()
    {
        ExpandPool(_numberToPreload);
    }

    public T Get()
    {
        if (_pool.Count > 0)
        {
            var entity = _pool.Dequeue();
            entity.Hide();
            return entity;
        }
        else
        {
            var entityScene = GD.Load<PackedScene>(_entityScenePath);
            var entity = entityScene.Instantiate<T>();
            AddChild(entity);
            return entity;
        }
    }
    
    public void Release(T entity)
    {
        entity.Hide();
        _pool.Enqueue(entity);
    }
    
    private void ExpandPool(int additionalCount)
    {
        var scene = GD.Load<PackedScene>(_entityScenePath);
        for (var i = 0; i < additionalCount; i++)
        {
            var entity = scene.Instantiate<T>();
            entity.Hide();
            AddChild(entity);
            _pool.Enqueue(entity);
        }
    }
}