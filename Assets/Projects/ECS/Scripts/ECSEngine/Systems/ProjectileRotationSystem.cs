using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSProject
{
    public sealed class ProjectileRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitTypeComponent, RigidbodyComponent>> _filter;
        
        private readonly EcsPoolInject<RotationComponent> _rotationPool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<UnitTypeComponent> typePool = _filter.Pools.Inc1;
            EcsPool<RigidbodyComponent> rigidbodyPool = _filter.Pools.Inc2;

            foreach (var entity in _filter.Value)
            {
                UnitTypes unitType = typePool.Get(entity).Value;
                
                if (unitType == UnitTypes.Projectile)
                {
                    Rigidbody rb = rigidbodyPool.Get(entity).Value;
                    Vector3 velocity = rb.linearVelocity;

                    if (velocity.sqrMagnitude > 0.001f)
                    {
                        Quaternion rotation = Quaternion.LookRotation(velocity);
                        rb.rotation = rotation;
                    }
                }
            }
        }
    }
}