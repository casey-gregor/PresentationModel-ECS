using System.Collections;
using System.Collections.Generic;
using ECSHomework;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Bullet : Entity
{
    public override int Id
    {
        get => _id;
        set => _id = value;
    }


    public override EcsWorld World
    {
        get => _world;
        set => _world = value;
    }

    public override ComponentsInstaller[] ComponentsInstallers
    {
        get => сomponentsInstallers;
        set => сomponentsInstallers = value;
    }

    private int _id = -1;
    private EcsWorld _world;
    
    [SerializeField] private ComponentsInstaller[] сomponentsInstallers;
    
}
