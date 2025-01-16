using ECSProject;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Projects.ECS.Scripts.ECSEngine.Systems
{
    public sealed class DelayedCheckSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DelayedCheckComponent, Timer, EntityObject>> _filter;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<DelayedCheckComponent> deathRequestPool = _filter.Pools.Inc1;
            EcsPool<Timer> timerPool = _filter.Pools.Inc2;
            
            foreach (var entity in _filter.Value)
            {
                ref float timeDelay = ref timerPool.Get(entity).Value;

                if (timeDelay <= 0f)
                {
                    deathRequestPool.Get(entity).Callback.Invoke();
                }
                
                timeDelay -= Time.deltaTime;
                
            }
        }
    }
}