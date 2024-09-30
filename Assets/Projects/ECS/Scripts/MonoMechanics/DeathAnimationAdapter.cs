using Projects.ECS.Scripts.ECSEngine.Components.UnitManagerComponents;
using UnityEngine;

namespace ECSHomework
{
    public class DeathAnimationAdapter : MonoBehaviour
    {
        private Entity Entity => GetComponent<Entity>();
        private void OnDeathEvent()
        {
            Entity.SetData(new ReturnToPool());
        }
    }
}