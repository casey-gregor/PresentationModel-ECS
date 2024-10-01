using System.Diagnostics.Tracing;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECSHomework
{
    public class Entity : MonoBehaviour
    {
        public int Id => _id;
        public EcsStartup EcsStartup => _ecsStartup;
        public EcsWorld World => _world;
        public ComponentsInstaller[] ComponentsInstallers => сomponentsInstallers;
        
        private int _id = -1;
        private EcsStartup _ecsStartup;
        private EcsWorld _world;
        
        [SerializeField] private ComponentsInstaller[] сomponentsInstallers;

        public void Initialize(EcsStartup ecsStartup, string worldName = null)
        {
            _ecsStartup = ecsStartup;
            _world = _ecsStartup.GetWorld(worldName);
            _id = _world.NewEntity();
            SetupData();
        }

        public void Initialize(int entityId, EcsWorld world)
        {
            _world = world;
            _id = entityId;
            SetupData();
        }
        
        private void SetupData()
        {
            if (ComponentsInstallers.Length != 0)
            {
                foreach (var installer in ComponentsInstallers)
                {
                    installer.Install();
                }
            }
        }

        public ref T GetData<T>() where T : struct
        {
            EcsPool<T> pool = World.GetPool<T>();
            ref T component = ref pool.Get(Id);
            return ref component;
        }

        public void SetData<T>(T component) where T : struct
        {
            EcsPool<T> pool = World.GetPool<T>();
            pool.Add(Id) = component;
        }

        public bool TrySetData<T>(T component) where T : struct
        {
            EcsPool<T> pool = World.GetPool<T>();
            if (!pool.Has(Id))
            {
                pool.Add(Id) = component;
                return true;
            }
            pool.Get(Id) = component;
            return false;
        }

        public bool TryDeleteData<T>() where T : struct
        {
            EcsPool<T> pool = World.GetPool<T>();
            if (pool.Has(Id))
            {
                pool.Del(Id);
                return true;
            }
            return false;
        }
    }
}