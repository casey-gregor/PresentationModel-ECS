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
