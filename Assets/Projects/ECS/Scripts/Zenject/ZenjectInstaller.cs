using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<AnotherTestZenject>().AsSingle().NonLazy();
        Container.Bind<TestZenject>().AsSingle().NonLazy();
        
    }
}
