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

    public Entity CreateEntity(Entity prefab, Vector3 position, Quaternion rotation)
    {
        Entity entity = GameObject.Instantiate(prefab, position, rotation);
        entity.Initialize(_world);
        return entity;
    }
}
