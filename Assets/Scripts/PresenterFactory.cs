using System;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public class PresenterFactory
    {
        private IPresenter currentPresenter;
        public IPresenter CurrentPresenter {  get { return currentPresenter; } }

        public event Action<IPresenter> presenterCreatedEvent;

        private PlayerPool playerManager;

        public PresenterFactory(PlayerPool playerManager)
        {
            this.playerManager = playerManager;
        }

        public IPresenter CreatePresenter(PlayerConfig playerConfig)
        {
            IPresenter presenter;

            if(this.playerManager.HasPlayer(playerConfig))
            {
                presenter = this.playerManager.GetPlayer(playerConfig) as IPresenter;
            }
            else
            {
                presenter = new Player(playerConfig);
                this.currentPresenter = presenter;
                this.playerManager.AddPlayer(this.currentPresenter as Player);
                presenterCreatedEvent?.Invoke(this.currentPresenter);
            }

            return presenter;
        }
   
    }

}
