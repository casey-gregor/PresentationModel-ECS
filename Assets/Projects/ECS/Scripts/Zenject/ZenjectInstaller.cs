using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace ECSHomework
{
    public class ZenjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EcsStartup>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
    
}
