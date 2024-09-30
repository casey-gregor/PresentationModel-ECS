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
        // public event Action<int, UnitType, Entity> UnitEnqueueEvent;
        // public event Action<Entity> GetUnitEvent;
        
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
                .Add(new TeamManagerSystem())
                .Add(new SpawnUnitRequestSystem())
                .Add(new SpawnUnitsSystem())
                .Add(new CheckGameOverSystem())
                
                .Add(new CheckIfCanMove())
                .Add(new MoveTowardsTargetSystem())
                .Add(new RotateTowardsTargetSystem())
                .Add(new MovementSystem())
                
                .Add(new CheckIfCanAttackSystem())
                .Add(new MeleeAttackSystem())
                .Add(new FireRequestSystem())
                .Add(new FireEventSystem())
                .Add(new TrajectoryCalculationSystem())
                .Add(new ProjectileRotationSystem())
                
                .Add(new DealDamageSystem())
                .Add(new GetDamageSystem())
                
                .Add(new RequestSetEntityInactive())
                .Add(new DeathAnimationRequestSystem())
                .Add(new DeathRequestSystem())
                
                //Game View
                .Add(new TransformViewSystem())
                .Add(new IsMovingCheckSystem())
                .Add(new AttackAnimationCheckSystem())
                .Add(new TakeDamageAnimationCheckSystem())
                .Add(new AnimationBoolParametersProcessingSystem())
                
                //VFX
                .Add(new ApplyFireVFXs())
                .Add(new ApplyBloodVFX())
                .Add(new ApplyHurtSound())
                .Add(new ApplyWeaponSound())
                .Add(new ApplyFireSound())
                .Add(new ApplyExplosionSound())
                
                .Add(new DeactivateDestroyedBaseSystem())
                .Add(new ReturnDeadUnitToPoolSystem())
                .Add(new SetEntityInactive())
                
                //Cleanup
                .Add(new OneFrameSystem())
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