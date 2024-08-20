using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECSHomework
{
    public abstract class Entity : MonoBehaviour
    {
        public abstract int ID { get; }
        public abstract void Initialize(EcsWorld world);
        public abstract ref T GetData<T>() where T : struct;
        public abstract void SetData<T>(T component) where T : struct;
    }
    public class Player : Entity
    {
        public override int ID => _id;
        private int _id = -1;
        
        [SerializeField] private GameObject character;
        [SerializeField] private float speed = 5f;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Entity bulletPrefab;
        
        private EcsWorld _world;

        public override void Initialize(EcsWorld world)
        {
            _world = world;

            int entity = world.NewEntity();
            _id = entity;

            EcsPool<MoveDirection> moveDirectionPool = world.GetPool<MoveDirection>();
            EcsPool<MoveSpeed> moveSpeedPool = world.GetPool<MoveSpeed>();
            EcsPool<Position> positionPool = world.GetPool<Position>();
            EcsPool<TransformView> transformViewPool = world.GetPool<TransformView>();
            EcsPool<BulletWeapon> weaponPool = world.GetPool<BulletWeapon>();


            MoveDirection moveDirection = moveDirectionPool.Add(entity);
            moveSpeedPool.Add(entity) = new MoveSpeed { Value = speed };
            positionPool.Add(entity) = new Position { Value = transform.position };
            transformViewPool.Add(entity) = new TransformView { Value = this.transform };
            weaponPool.Add(entity) = new BulletWeapon { Firepoint = firePoint, BulletPrefab = bulletPrefab };
        }

        public override ref T GetData<T>() where T : struct
        {
            EcsPool<T> pool = _world.GetPool<T>();
            ref T component = ref pool.Get(_id);
            return ref component;
        }

        public override void SetData<T>(T component) where T : struct
        {
            EcsPool<T> componentPool = _world.GetPool<T>();
            componentPool.Add(_id);
        }
    }
}