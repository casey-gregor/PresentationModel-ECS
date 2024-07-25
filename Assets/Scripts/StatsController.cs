using System;
using UniRx;

namespace Lessons.Architecture.PM
{

    public class StatsController : IDisposable
    {
        private PresenterFactory presenterFactory;
        private ActionHelper actionHelper;

        private IDisposable disposable;
        public StatsController(PresenterFactory presenterFactory, ActionHelper actionHelper)
        {
            this.presenterFactory = presenterFactory;
            this.actionHelper = actionHelper;

            this.presenterFactory.PresenterCreatedEvent += HandlePresenterCreatedEvent;
        }

        private void HandlePresenterCreatedEvent(IPresenter presenter)
        {
            PlayerPresenter newPresenter = presenter as PlayerPresenter;
            this.disposable = newPresenter.LevelPresenter.Level.SkipLatestValueOnSubscribe().Subscribe(UpdateStats);
        }

        private void UpdateStats(int _)
        {
            PlayerPresenter currentPresenter = this.presenterFactory.CurrentPresenter as PlayerPresenter;
            PlayerStat[] currentStats = currentPresenter.StatsPresenter.GetStats();
            currentPresenter.StatsPresenter.UpdateStats(currentStats, this.actionHelper.StatToAdd);

        }
        public void Dispose()
        {
            this.presenterFactory.PresenterCreatedEvent -= HandlePresenterCreatedEvent;
            this.disposable.Dispose();
        }

    }
}
