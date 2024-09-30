using UnityEngine;

namespace ECSHomework
{
    [RequireComponent(typeof(Entity))]
    public class FireProjectileAnimationAdapter : MonoBehaviour
    {
        private Entity Entity => GetComponent<Entity>();
        private void OnAttackEvent()
        {
            Entity.SetData(new FireRequest());
        }
    }
}