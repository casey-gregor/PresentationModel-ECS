using System;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class PresenterFactory
    {
        public event Action<IPresenter> PresenterCreatedEvent;
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

            if (this.playerPool.HasPlayer(playerConfig))
            {
                Debug.Log("Added existing presenter");
                this.currentPresenter = this.playerPool.GetPlayer(playerConfig);

            }
            else
            {
                Debug.Log("Created new presenter");
                this.currentPresenter = this.diContainer.Instantiate<PlayerPresenter>(new object[] { playerConfig, this.diContainer });
                this.playerPool.AddPlayer(this.currentPresenter as PlayerPresenter);
                this.PresenterCreatedEvent?.Invoke(this.currentPresenter);
            }

            return this.currentPresenter;
        }
   
    }

}
