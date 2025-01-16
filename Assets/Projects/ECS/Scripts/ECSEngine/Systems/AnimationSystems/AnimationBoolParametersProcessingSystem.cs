using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSProject
{
    public sealed class AnimationBoolParametersProcessingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AnimatorComponent>> _filter;

        private readonly EcsPoolInject<IsMoving> _isMovingPool;
        private readonly EcsPoolInject<IsDeadAnimation> _isDeadPool;
        
        private readonly int _isDeadAnimationParameter = Animator.StringToHash("IsDead");
        private readonly int _isWalkingAnimationParameter = Animator.StringToHash("IsWalking");
        
        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                Animator animator = _filter.Pools.Inc1.Get(entity).Value;
                
                if (_isDeadPool.Value.Has(entity))
                {
                    if(animator.GetBool(_isDeadAnimationParameter) == false)
                        animator.SetBool(_isDeadAnimationParameter, true);
                }
                else
                {
                    animator.SetBool(_isDeadAnimationParameter, false);
                }
                
                if (_isMovingPool.Value.Has(entity))
                {
                    _isMovingPool.Value.Del(entity);
                    if(animator.GetBool(_isWalkingAnimationParameter) == false)
                        animator.SetBool(_isWalkingAnimationParameter, true);
                }
                else
                { 
                    animator.SetBool(_isWalkingAnimationParameter, false);
                }
                
            }
        }
    }
}