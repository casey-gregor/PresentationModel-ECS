using UnityEngine;

namespace ECSHomework
{
    [RequireComponent(typeof(Entity))]
    public abstract class ComponentsInstaller : MonoBehaviour
    {
        public Entity Entity => GetComponent<Entity>();
        
        public abstract void Install();
    }
}