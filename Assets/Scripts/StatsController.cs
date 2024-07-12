
using Lessons.Architecture.PM;

public class StatsController
{
    private PresenterFactory presenterFactory;
    private ActionHelper actionHelper;
    public StatsController(PresenterFactory presenterFactory, ActionHelper actionHelper)
    {
        this.presenterFactory = presenterFactory;
        this.actionHelper = actionHelper;

        this.presenterFactory.presenterCreatedEvent += HandlePresenterCreatedEvent;
    }

    private void HandlePresenterCreatedEvent(IPresenter presenter)
    {
        presenter.PlayerLevel.OnLevelUp += UpdateStats;
    }

    private void UpdateStats()
    {
        IPresenter currentPresenter = this.presenterFactory.CurrentPresent;
        PlayerStat[] currentStats = currentPresenter.Stats.GetStats();
        foreach (PlayerStat stat in currentStats)
        {
            int currentValue = stat.Value;
            stat.ChangeValue(currentValue + actionHelper.StatToAdd);
        }
    }
}
