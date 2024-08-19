using ECSHomework;
using Leopotam.EcsLite;
using UnityEngine;

public class EntityManager
{
    private EcsWorld _world;
    
    
    public void Initialize(EcsWorld world)
    {
        var entities = GameObject.FindObjectsOfType<Entity>();

        foreach (var entity in entities)
        {
            entity.Initialize(world);
        }
        
        _world = world;
    }
}
