using System;

namespace GodotBackgroundSimulation.Scripts.GameEntities;

public class GameEntityId
{
    public string Id { get; set; } = GenerateId();

    private static string GenerateId()
    {
        return Guid.NewGuid().ToString();
    }
}