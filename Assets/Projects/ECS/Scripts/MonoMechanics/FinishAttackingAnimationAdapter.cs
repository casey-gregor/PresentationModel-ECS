using UnityEngine;

namespace ECSProject
{
    [RequireComponent(typeof(Entity))]
    public class FinishAttackingAnimationAdapter : MonoBehaviour
    {
        private Entity Entity => GetComponent<Entity>();
        private void OnFiniishAttackingAnimation()
        {
            Entity.TryDeleteData<IsAttacking>();
        }
    }
}