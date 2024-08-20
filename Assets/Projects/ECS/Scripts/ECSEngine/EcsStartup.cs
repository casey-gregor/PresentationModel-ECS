using ECSHomework.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework 
{
    public sealed class EcsStartup : MonoBehaviour 
    {
        private EcsSystems _systems;
        private EcsWorld _world;
        private EcsWorld _eventsWorld;
        private EntityManager _entityManager;
      
        private void Start () 
        {        
            // register your shared data here, for example:
            // var shared = new Shared ();
            // systems = new EcsSystems (new EcsWorld (), shared);
            _world = new EcsWorld();
            _eventsWorld = new EcsWorld();
            _systems = new EcsSystems (_world);
            _systems.AddWorld(_eventsWorld, EcsWorlds.EVENTS_WORLD);
            _entityManager = new EntityManager();
            _systems
                .Add(new MovementSystem())
                .Add(new ExampleSystem())
                .Add(new FireRequestSystem())
                .Add(new BulletSpawnSystem())
                
                
                
                
                
                .Add(new TransformViewSystem())
                // register your systems here, for example:
                // .Add (new TestSystem1 ())
                // .Add (new TestSystem2 ())
                
                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                
                .Inject()
                .Inject(_entityManager)
                .Init();
            _entityManager.Initialize(_world);
        }

        private void Update () 
        {
            _systems?.Run ();
        }

        private void OnDestroy () 
        {
            if (_systems != null) {
                _systems.Destroy ();
                // add here cleanup for custom worlds, for example:
                // _systems.GetWorld ("events").Destroy ();
                _systems.GetWorld ().Destroy ();
                _systems = null;
            }
        }
    }
}