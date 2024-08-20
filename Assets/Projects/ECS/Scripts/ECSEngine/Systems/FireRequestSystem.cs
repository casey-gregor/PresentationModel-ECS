using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public sealed class FireRequestSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<FireRequest, BulletWeapon>> _filter;
        
        private EcsPoolInject<SpawnRequest> _spawnPool;

        public void Run(EcsSystems systems)
        {
            EcsPool<FireRequest> fireRequestPool = _filter.Pools.Inc1;
            EcsPool<BulletWeapon> weaponPool = _filter.Pools.Inc2;
            
            foreach (int entity in _filter.Value)
            {
                _spawnPool.Value.Add(entity) = new SpawnRequest();

                fireRequestPool.Del(entity);
            }
        }
    }
}