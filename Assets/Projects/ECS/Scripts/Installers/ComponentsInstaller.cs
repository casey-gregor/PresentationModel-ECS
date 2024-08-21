using Leopotam.EcsLite;
using UnityEngine;

namespace ECSHomework
{
    public abstract class ComponentsInstaller : MonoBehaviour
    {
        public abstract void Install(EcsWorld world);
    }
}