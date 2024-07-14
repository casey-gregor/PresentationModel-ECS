
using System;

namespace Lessons.Architecture.PM
{

    public class StatsController : IDisposable
    {
        private PresenterFactory presenterFactory;
        private ActionHelper actionHelper;
        private IPresenter presenter;
        public StatsController(PresenterFactory presenterFactory, ActionHelper actionHelper)
        {
            this.presenterFactory = presenterFactory;
            this.actionHelper = actionHelper;

            this.presenterFactory.presenterCreatedEvent += HandlePresenterCreatedEvent;
        }

        private void HandlePresenterCreatedEvent(IPresenter presenter)
        {
            this.presenter = presenter;
            this.presenter.PlayerLevel.OnLevelUp += UpdateStats;
        }

        private void UpdateStats()
        {
            IPresenter currentPresenter = this.presenterFactory.CurrentPresenter;
            PlayerStat[] currentStats = currentPresenter.Stats.GetStats();
            foreach (PlayerStat stat in currentStats)
            {
                int currentValue = stat.Value;
                stat.ChangeValue(currentValue + actionHelper.StatToAdd);
            }
        }
        public void Dispose()
        {
            this.presenterFactory.presenterCreatedEvent -= HandlePresenterCreatedEvent;
            this.presenter.PlayerLevel.OnLevelUp -= UpdateStats;
        }

    }
}
