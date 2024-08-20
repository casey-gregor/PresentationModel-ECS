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

    public int CreateEntity(Entity prefab, Transform spawnPosition)
    {
        Debug.Log("Creating entity");
        Entity entity = GameObject.Instantiate(prefab, spawnPosition.position, spawnPosition.rotation);
        entity.Initialize(_world);
        return entity.ID;
    }
}
