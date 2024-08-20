using System.Collections;
using System.Collections.Generic;
using ECSHomework;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

public class Bullet : Entity
{
    public override int ID => _id;
    private int _id = -1;
    private EcsWorld _world;
    
    [SerializeField] private float speed = 15f;
    public override void Initialize(EcsWorld world)
    {
        _world = world;
        int entity = _world.NewEntity();
        _id = entity;

        EcsPool<MoveSpeed> moveSpeedPool = _world.GetPool<MoveSpeed>();
        EcsPool<MoveDirection> moveDirectionPool = _world.GetPool<MoveDirection>();
        EcsPool<Position> positionPool = _world.GetPool<Position>();
        EcsPool<Rotation> rotationPool = _world.GetPool<Rotation>();
        EcsPool<TransformView> transformPool = _world.GetPool<TransformView>();

        moveSpeedPool.Add(entity) = new MoveSpeed { Value = speed };
        moveDirectionPool.Add(entity) = new MoveDirection { Value = new Vector3(1f, 0f, 0f) };

        transformPool.Add(entity) = new TransformView { Value = this.transform };
    }
    
    public override ref T GetData<T>()
    {
        EcsPool<T> pool = _world.GetPool<T>();
        ref T component = ref pool.Get(_id);
        return ref component;
    }

    public override void SetData<T>(T component)
    {
        EcsPool<T> pool = _world.GetPool<T>();
        pool.Add(_id);
    }
}
