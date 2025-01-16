using Projects.ECS.Scripts.ECSEngine.Components.UnitManagerComponents;
using UnityEngine;

namespace ECSProject
{
    public class DeathAnimationAdapter : MonoBehaviour
    {
        private Entity Entity => GetComponent<Entity>();
        private void OnDeathEvent()
        {
            Entity.TrySetData(new ReturnToPool());
        }
    }
}