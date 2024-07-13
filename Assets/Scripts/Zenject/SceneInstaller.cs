using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private ActionHelper actionHelper;
        public override void InstallBindings()
        {
            Container.Bind<PresenterFactory>().AsSingle().NonLazy();
            Container.Bind<XPController>().AsSingle().NonLazy();
            Container.Bind<StatsController>().AsSingle().WithArguments(actionHelper).NonLazy();
        }
    }

}
