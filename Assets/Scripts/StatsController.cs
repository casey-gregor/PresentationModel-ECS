using System;
using UniRx;

namespace Lessons.Architecture.PM
{

    public class StatsController : IDisposable
    {
        private PresenterFactory presenterFactory;
        private ActionHelper actionHelper;
        private IPresenter presenter;
        private IDisposable disposable;
        public StatsController(PresenterFactory presenterFactory, ActionHelper actionHelper)
        {
            this.presenterFactory = presenterFactory;
            this.actionHelper = actionHelper;

            this.presenterFactory.presenterCreatedEvent += HandlePresenterCreatedEvent;
        }

        private void HandlePresenterCreatedEvent(IPresenter presenter)
        {
            this.presenter = presenter;
            this.disposable = this.presenter.Level.Subscribe(UpdateStats);
        }

        private void UpdateStats(int _)
        {
            IPresenter currentPresenter = this.presenterFactory.CurrentPresenter;
            PlayerStat[] currentStats = currentPresenter.GetStats();
            foreach (PlayerStat stat in currentStats)
            {
                int currentValue = stat.Value;
                stat.ChangeValue(currentValue + this.actionHelper.StatToAdd);
            }
        }
        public void Dispose()
        {
            this.presenterFactory.presenterCreatedEvent -= HandlePresenterCreatedEvent;
            this.disposable.Dispose();
        }

    }
}
