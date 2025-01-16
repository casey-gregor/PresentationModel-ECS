using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSProject
{
    public sealed class OneFrameSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS_WORLD;
        
        private readonly EcsFilterInject<Inc<OneFrame>> _filter = EcsWorlds.EVENTS_WORLD;
        
        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
}