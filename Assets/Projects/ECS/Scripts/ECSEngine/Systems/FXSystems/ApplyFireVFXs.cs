using ECSProject.VFX;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSProject
{
    public sealed class ApplyFireVFXs : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<Health, IsBase>, Exc<Inactive>> _filter;
        
        private readonly EcsPoolInject<FireSmallVFX> _fireSmallVFXPool;
        private readonly EcsPoolInject<FireMediumVFX> _fireMediumVFXPool;
        private readonly EcsPoolInject<BuildingDestroyVFX> _buildingDestroyVFXPool;
        private readonly EcsPoolInject<TeamComponent> _teamComponentPool;
        private readonly EcsPoolInject<EntityObject> _entityObjectPool;
        
        private readonly EcsPoolInject<Timer> _timeDelayPool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<Health> healthPool = _filter.Pools.Inc1;
          
            foreach (var entity in _filter.Value)
            {
              
                int initialHealth = healthPool.Get(entity).InitialValue;
                int currentHealth = healthPool.Get(entity).CurrentValue;
                
                int leftHealthPercent = (currentHealth * 100) / initialHealth;
                

                if (leftHealthPercent != 100)
                {
                    ParticleSystem fireSmall = _fireSmallVFXPool.Value.Get(entity).Value;
                    ParticleSystem fireMedium = _fireMediumVFXPool.Value.Get(entity).Value;
                    ParticleSystem buildingDestroy = _buildingDestroyVFXPool.Value.Get(entity).Value;
                        
                    if (leftHealthPercent <= 70 && leftHealthPercent > 50 && fireSmall.isStopped)
                    {
                        fireSmall.Play();
                    }
                    else if (leftHealthPercent <= 50 && leftHealthPercent > 0 && fireMedium.isStopped)
                    {
                        fireMedium.Play();
                    }
                    else if (currentHealth <= 0 && buildingDestroy.isStopped)
                    {
                        fireSmall.Stop();
                        fireMedium.Stop();
                        buildingDestroy.Play();
                    }
                }
                    
            }
        }
    }
}