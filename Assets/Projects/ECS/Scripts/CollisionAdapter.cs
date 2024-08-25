using System;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace ECSHomework
{
    [RequireComponent(typeof(Entity))]
    public class CollisionAdapter : MonoBehaviour
    {
        private Entity _entity;
        
        private void Awake()
        {
            _entity = GetComponent<Entity>();
        }

        private void OnTriggerEnter(Collider collision)
        {
            collision.gameObject.transform.parent.TryGetComponent<Entity>(out Entity entity);
            if (entity != null)
            {
                EcsWorld world = _entity.EcsStartup.GetWorld(EcsWorlds.EVENTS_WORLD);
                EcsPool<DeathRequest> deathRequestPool = _entity.World.GetPool<DeathRequest>();
                
                deathRequestPool.Add(entity.Id);
            }
        }
    }
}