using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Projects.ECS.Scripts.ECSEngine.Components.UnitManagerComponents;

namespace ECSProject
{
    public sealed class ReturnDeadUnitToPoolSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ReturnToPool, UnitTypeComponent, EntityObject, TeamManagerComponent>> _filter;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<UnitTypeComponent> typePool = _filter.Pools.Inc2;
            EcsPool<EntityObject> entityPool = _filter.Pools.Inc3;
            EcsPool<TeamManagerComponent> teamManagerPool = _filter.Pools.Inc4;

            foreach (var entity in _filter.Value)
            {
                UnitTypes type = typePool.Get(entity).Value;
                TeamManager teamManager = teamManagerPool.Get(entity).Value;
                Entity unit = entityPool.Get(entity).Value;
                teamManager.ReturnUnit(type, unit);
                
                systems.GetWorld().DelEntity(entity);
            }
        }
    }
}