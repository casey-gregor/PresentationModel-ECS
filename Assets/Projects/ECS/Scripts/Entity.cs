using Leopotam.EcsLite;
using UnityEngine;

namespace ECSHomework
{
    public abstract class Entity : MonoBehaviour
    {
        public abstract EcsWorld World { get; set; }
        public abstract int Id { get; set; }
        public abstract ComponentsInstaller[] ComponentsInstallers { get; set; }

        public virtual void Initialize(EcsWorld world)
        {
            World = world;
            Id = world.NewEntity();
            
            SetupData();
        }
        
        protected virtual void SetupData()
        {
            if (ComponentsInstallers.Length != 0)
            {
                foreach (var installer in ComponentsInstallers)
                {
                    installer.Install(World);
                }
            }
        }

        public virtual ref T GetData<T>() where T : struct
        {
            EcsPool<T> pool = World.GetPool<T>();
            ref T component = ref pool.Get(Id);
            return ref component;
        }

        public virtual void SetData<T>(T component) where T : struct
        {
            EcsPool<T> pool = World.GetPool<T>();
            pool.Add(Id) = component;
        }
    }
}