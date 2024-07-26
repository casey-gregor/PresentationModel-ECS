using System.Collections.Generic;
using Zenject;

namespace Lessons.Architecture.PM
{

    public class PlayerPresenter : IBigPresenter
    {
        public IReadOnlyList<ISmallPresenter> SmallPresenters => smallPresenters;
        private List<ISmallPresenter> smallPresenters = new();

        public PlayerConfig Config => config;
        private PlayerConfig config;

        private UserInfoPresenter userInfoPresenter;

        public LevelPresenter LevelPresenter => levelPresenter;
        private LevelPresenter levelPresenter;

        public AllStatsPresenter StatsPresenter => statsPresenter;
        private AllStatsPresenter statsPresenter;

        public PlayerPresenter(PlayerConfig config, DiContainer diContainer)
        {
            this.config = config;

            this.smallPresenters.Clear();

            this.userInfoPresenter = new UserInfoPresenter(config);
            this.smallPresenters.Add(this.userInfoPresenter);

            this.levelPresenter = diContainer.Instantiate<LevelPresenter>(new object[] {config, diContainer} );
            this.smallPresenters.Add(this.levelPresenter);

            this.statsPresenter = diContainer.Instantiate<AllStatsPresenter>(new object[] { config, diContainer });
            this.smallPresenters.Add(this.statsPresenter);
        } 
       
    }

}
