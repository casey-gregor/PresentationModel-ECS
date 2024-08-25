using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECSHomework
{
    [RequireComponent(typeof(Entity))]
    public abstract class ComponentsInstaller : MonoBehaviour
    {
        public Entity Entity {get; private set;}
        public void Awake()
        {
            Entity = GetComponent<Entity>();
        }

        public abstract void Install();
    }
}