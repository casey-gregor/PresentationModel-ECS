using ECSProject;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Projects.ECS.Scripts.ECSEngine.Systems
{
    public sealed class ResetAttackTimerSystem  : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<MoveAllowed, MoveDirection, MoveSpeed, PositionComponent>, 
            Exc<ReadyForAttack, IsAttacking, GameOver, Inactive>> _filter;

        private readonly EcsPoolInject<Timer> _timerPool;
        
        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                if (_timerPool.Value.Has(entity))
                {
                    ref float timer = ref _timerPool.Value.Get(entity).Value;
                    timer = 0;
                }
            }
        }
    }
}