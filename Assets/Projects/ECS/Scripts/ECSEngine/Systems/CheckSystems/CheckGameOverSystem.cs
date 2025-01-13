using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public sealed class CheckGameOverSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EntityObject, Inactive, IsBase>, Exc<GameOver>> _deadFilter;
        
        private readonly EcsFilterInject<Inc<EntityObject>, Exc<DeathRequest, GameOver>> _aliveEntitiesFilter;
        
        private readonly EcsPoolInject<GameOver> _gameOverPool;
        

        public void Run(EcsSystems systems)
        {
            foreach (var deadEntity in _deadFilter.Value)
            {
                foreach (var entity in _aliveEntitiesFilter.Value)
                {
                    _gameOverPool.Value.Add(entity);
                }
            }
        }
    }
}