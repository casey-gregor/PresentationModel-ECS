using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSProject
{
    public sealed class MeleeAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackAllowed, TargetEntity, Timer, ReadyForAttack>> _filter;

        private readonly EcsPoolInject<AttackInterval> _attackIntervalPool;
        private readonly EcsPoolInject<Health> _healthPool;

        private readonly EcsWorldInject _eventsWorld = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<TriggerAttack> _triggerAttackPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<OneFrame> _oneFramePool = EcsWorlds.EVENTS_WORLD;
        
        public void Run(EcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            
            EcsPool<TargetEntity> targetEntityPool = _filter.Pools.Inc2;
            EcsPool<Timer> attackTimerPool = _filter.Pools.Inc3;

            foreach (var entity in _filter.Value)
            {
                ref float attackTimer = ref attackTimerPool.Get(entity).Value;
                Entity targetEntity = targetEntityPool.Get(entity).Value;
               
                if (targetEntity != null)
                {
                    if (attackTimer <= 0)
                    {
                        int newEntity = _eventsWorld.Value.NewEntity();
                        _triggerAttackPool.Value.Add(newEntity) = new TriggerAttack { Entity = entity };
                        _oneFramePool.Value.Add(newEntity);//Should be deleted after one frame
                       
                        attackTimer = _attackIntervalPool.Value.Get(entity).Value;
                    }
                    attackTimer -= deltaTime;
                }
            }
        }
        
    }
}