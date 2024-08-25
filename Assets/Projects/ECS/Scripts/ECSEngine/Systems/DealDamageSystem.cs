using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEditor;
using UnityEngine;

namespace ECSHomework
{
    public class DealDamageSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TargetEntity, ReadyForAttack, AttackDamage>> _filter;
        private EcsPoolInject<Health> _healthPool;

        private float damageTimerSet = 1.0f;
        private float timer;
        
        public void Run(EcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            
            EcsPool<TargetEntity> targetEntityPool = _filter.Pools.Inc1;
            EcsPool<AttackDamage> attackDamagePool = _filter.Pools.Inc3;

            foreach (var entity in _filter.Value)
            {
                Entity targetEntity = targetEntityPool.Get(entity).Value;
                if (targetEntity != null)
                {
                    ref int targetHealth = ref _healthPool.Value.Get(targetEntity.Id).Value;
                    int attackDamage = attackDamagePool.Get(entity).Value;

                    if (timer <= 0)
                    {
                        targetHealth = Mathf.Max(0, targetHealth-attackDamage);
                        timer = damageTimerSet;
                        Debug.Log("attacking");
                    }
                    
                    timer -= deltaTime;
                    
                }
            }
        }
    }
}