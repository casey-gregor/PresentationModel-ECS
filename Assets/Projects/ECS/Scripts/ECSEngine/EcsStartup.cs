using System;
using ECSHomework.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace ECSHomework 
{
    public sealed class EcsStartup : MonoBehaviour
    {
        private EcsSystems _systems;
        private EcsWorld _world;
        private EcsWorld _eventsWorld;
        private EntityManager _entityManager;
      
        private void Awake () 
        {    
            _world = new EcsWorld();
            _eventsWorld = new EcsWorld();
            _systems = new EcsSystems (_world);
            _systems.AddWorld(_eventsWorld, EcsWorlds.EVENTS_WORLD);
            _entityManager = new EntityManager();
            _systems
                //Game Logic
                .Add(new MoveTowardsTargetSystem())
                .Add(new RotateTowardsTargetSystem())
                // .Add(new IdentifyAttackDistanceSystem())
                .Add(new MovementSystem())
                .Add(new DealDamageSystem())
                .Add(new CheckTargetAlive())
                // .Add(new FireRequestSystem())
                // .Add(new BulletSpawnSystem())
                .Add(new DeathRequestSystem())
                .Add(new DeathEventSystem())
                
                
                
                //Game View
                .Add(new TransformViewSystem())
                //Unity Editor
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                
                .Inject()
                .Inject(_entityManager)
                .Init();
            _entityManager.Initialize(this);
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

        public EcsWorld GetWorld(string worldName = null)
        {
            return _systems.GetWorld(worldName);
        }

        public EntityBuilder CreateEntity(string worldName = null)
        {
            return new EntityBuilder(_systems.GetWorld(worldName));
        }
    }
}