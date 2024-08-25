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

        public virtual void Initialize(EcsStartup ecsStartup, string worldName = null)
        {
            _ecsStartup = ecsStartup;
            _world = _ecsStartup.GetWorld(worldName);
            _id = _world.NewEntity();
            // Debug.Log("id : " + Id);
            SetupData();
        }
        
        protected virtual void SetupData()
        {
            if (ComponentsInstallers.Length != 0)
            {
                foreach (var installer in ComponentsInstallers)
                {
                    installer.Install();
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