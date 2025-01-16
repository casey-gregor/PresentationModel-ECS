using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSProject
{
    public sealed class FireRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<AttackAllowed, ShootingWeapon, TargetEntity, Timer>, 
                Exc<Inactive, DeathRequest, GameOver>> _filter;
        
        private readonly EcsPoolInject<Health> _healthPool;
        
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<TriggerAttack> _isAttackingPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<OneFrame> _oneFramePool = EcsWorlds.EVENTS_WORLD;
        
        private float _timer;
        
        public void Run(EcsSystems systems)
        {
            float deltaTime = Time.deltaTime;

            EcsPool<ShootingWeapon> shootingWeaponPool = _filter.Pools.Inc2;
            EcsPool<TargetEntity> targetEntityPool = _filter.Pools.Inc3;
            EcsPool<Timer> fireTimerPool = _filter.Pools.Inc4;
            
            foreach (var entity in _filter.Value)
            {
                ref float attackTimer = ref fireTimerPool.Get(entity).Value;
                Entity targetEntity = targetEntityPool.Get(entity).Value;


                if (targetEntity != null)
                {
                    int targetHealth = _healthPool.Value.Get(targetEntity.Id).CurrentValue;
                    if (targetHealth <= 0)
                    {
                        attackTimer = 0;
                        return;
                    }
                    
                    if (attackTimer <= 0)
                    {
                        int newEntity = _eventWorld.Value.NewEntity();
                        _isAttackingPool.Value.Add(newEntity) = new TriggerAttack { Entity = entity };
                        _oneFramePool.Value.Add(newEntity);
        
                        attackTimer = shootingWeaponPool.Get(entity).FireRate;
                    }
                    
                    attackTimer -= deltaTime;
                    
                }
                
                
            }
        }
    }
}