using System;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class PresenterFactory
    {
        public event Action<IPresenter> presenterCreatedEvent;
        public IPresenter CurrentPresenter {  get { return currentPresenter; } }
        private IPresenter currentPresenter;

        private PlayerPool playerPool;
        private DiContainer diContainer;

        public PresenterFactory(PlayerPool playerPool, DiContainer container)
        {
            this.playerPool = playerPool;
            this.diContainer = container;
        }

        public IPresenter CreatePresenter(PlayerConfig playerConfig)
        {

            if(this.playerPool.HasPlayer(playerConfig))
            {
                this.currentPresenter = this.playerPool.GetPlayer(playerConfig);
                
            }
            else
            {
                this.currentPresenter = this.diContainer.Instantiate<Player>(new object[] { playerConfig });
                this.playerPool.AddPlayer(this.currentPresenter as Player);
                this.presenterCreatedEvent?.Invoke(this.currentPresenter);
            }

            return this.currentPresenter;
        }
   
    }

}
