using System.Collections.Generic;
using Godot;
using godotbackgroundsimulation.Scripts.Interfaces;

namespace GodotBackgroundSimulation.Scripts.GameEntities.Creatures;

public class Creature : GameEntity, IHourlyUpdatedEntity
{
    public Vector2 Velocity { get; set; }
    public Vector2 TargetPosition { get; set; }
    public int CurrentHealth { get; set; }

    public void HourlyUpdateTasks()
    {
        // TODO: do meaningful stuff

        CurrentHealth += 1;
        var velocityChoices = new List<Vector2> {Vector2.Right, Vector2.Up, Vector2.Down, Vector2.Left};

        var rng = new Godot.RandomNumberGenerator();
        rng.Randomize();
        var randomIndex = (int)rng.RandiRange(0, velocityChoices.Count);
        Velocity = velocityChoices[randomIndex];
    }
}