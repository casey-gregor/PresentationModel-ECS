using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private ActionHelper actionHelper;
        public override void InstallBindings()
        {
            Container.Bind<IViewable>().To<UserInfoView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IViewable>().To<LevelView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IViewable>().To<StatView>().FromComponentInHierarchy().AsSingle();
            //Container.BindInterfacesAndSelfTo<LevelPresenter>().AsTransient();
            Container.Bind<PopupView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<PlayerPool>().AsSingle().NonLazy();
            Container.Bind<PresenterFactory>().AsSingle().NonLazy();
            Container.Bind<XPController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StatsController>().AsSingle().WithArguments(actionHelper).NonLazy();
        }
    }

}
