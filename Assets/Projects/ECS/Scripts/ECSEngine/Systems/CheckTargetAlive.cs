using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECSHomework
{
    public class CheckTargetAlive : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TargetEntity, Health>, Exc<DeathRequest, Inactive>> _filter;

        private EcsPoolInject<ReadyForAttack> _readyForAttackPool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<TargetEntity> targetEntityPool = _filter.Pools.Inc1;
            EcsPool<Health> healthPool = _filter.Pools.Inc2;

            foreach (var entity in _filter.Value)
            {
                Entity targetEntity = targetEntityPool.Get(entity).Value;
                if (targetEntity != null)
                {
                    int targetHealth = healthPool.Get(targetEntity.Id).Value;

                    bool targetAlive = targetHealth > 0;

                    if (!targetAlive)
                    {
                        targetEntityPool.Get(entity).Value = null;
                        _readyForAttackPool.Value.Del(entity);
                    }
                }
            }
        }
    }
}