using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public class DeathRequestSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Health, EntityObject>, Exc<DeathRequest, Inactive>> _filter;
        
        private EcsPoolInject<DeathRequest> _deathRequestPool;
         
        public void Run(EcsSystems systems)
        {
            EcsPool<Health> healthPool = _filter.Pools.Inc1;

            foreach (int entity in _filter.Value)
            {
                int health = healthPool.Get(entity).Value;

                if (health <= 0)
                {
                    // Debug.Log("Death Request : " + _deathRequestPool);
                    _deathRequestPool.Value.Add(entity) = new DeathRequest();
                }
            }
        }
    }
}