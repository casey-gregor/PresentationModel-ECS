using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public sealed class BulletSpawnSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<SpawnRequest, BulletWeapon>> _filter;
        
        private EcsPoolInject<Position> _positionPool;
        private EcsPoolInject<Rotation> _rotationPool;
        
        private EcsCustomInject<EntityManager> _entityManager;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<SpawnRequest> spawnEventPool = _filter.Pools.Inc1;
            EcsPool<BulletWeapon> bulletWeaponPool = _filter.Pools.Inc2;
            
            EntityManager entityManager = _entityManager.Value;

            foreach (int entity in _filter.Value)
            {
                
                BulletWeapon bulletWeapon = bulletWeaponPool.Get(entity);
                
                int newBulletEntity = entityManager.CreateEntity(bulletWeapon.BulletPrefab, bulletWeapon.Firepoint);
                
                _positionPool.Value.Add(newBulletEntity) = new Position { Value = bulletWeapon.Firepoint.position };
                _rotationPool.Value.Add(newBulletEntity) = new Rotation { Value = bulletWeapon.Firepoint.rotation };
                
                spawnEventPool.Del(entity);
            }
        }
    }
}