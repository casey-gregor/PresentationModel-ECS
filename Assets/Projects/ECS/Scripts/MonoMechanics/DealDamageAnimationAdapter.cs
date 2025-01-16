using UnityEngine;

namespace ECSProject
{
    [RequireComponent(typeof(Entity))]
    public class DealDamageAnimationAdapter : MonoBehaviour
    {
        private Entity Entity => GetComponent<Entity>();
        private void OnAttackEvent()
        {
            Entity.TrySetData(new DealDamageRequest());
        }
    }
}