using ECSHomework;
using Leopotam.EcsLite;
using UnityEngine;

public class EntityManager
{
    private EcsStartup _ecsStartup;
    
    public void Initialize(EcsStartup ecsStartup)
    {
        var entities = GameObject.FindObjectsOfType<Entity>();

        foreach (var entity in entities)
        {
            entity.Initialize(ecsStartup);
        }
        
        _ecsStartup = ecsStartup;
    }

    public Entity CreateEntity(Entity prefab, Vector3 position, Quaternion rotation)
    {
        Entity entity = GameObject.Instantiate(prefab, position, rotation);
        entity.Initialize(_ecsStartup);
        return entity;
    }

    public void DestroyEntity(Entity entity)
    {
        _ecsStartup.GetWorld().DelEntity(entity.Id);
        GameObject.Destroy(entity.gameObject);
    }
    
}
