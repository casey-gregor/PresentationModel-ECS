using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECSHomework
{
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
            _id = world.NewEntity();
            
            SetData(new MoveDirection());
            SetData(new MoveSpeed { Value = speed });
            SetData(new Position { Value = transform.position });
            SetData(new TransformView { Value = transform });
            SetData(new BulletWeapon { Firepoint = firePoint, BulletPrefab = bulletPrefab });
            
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
            componentPool.Add(_id) = component;
        }
    }
}