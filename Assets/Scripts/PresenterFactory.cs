using System;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class PresenterFactory
    {
        public event Action<IBigPresenter> PresenterCreatedEvent;
        public IBigPresenter CurrentPresenter {  get { return currentPresenter; } }
        private IBigPresenter currentPresenter;

        private PlayerPool playerPool;
        private DiContainer diContainer;

        public PresenterFactory(PlayerPool playerPool, DiContainer container)
        {
            this.playerPool = playerPool;
            this.diContainer = container;
        }

        public IBigPresenter CreatePresenter(PlayerConfig playerConfig)
        {

            if (this.playerPool.HasPlayer(playerConfig))
            {
                this.currentPresenter = this.playerPool.GetPlayer(playerConfig);
            }
            else
            {
                this.currentPresenter = this.diContainer.Instantiate<PlayerPresenter>(new object[] { playerConfig, this.diContainer });
                this.playerPool.AddPlayer(this.currentPresenter as PlayerPresenter);
                this.PresenterCreatedEvent?.Invoke(this.currentPresenter);
            }

            return this.currentPresenter;
        }
   
    }

}
