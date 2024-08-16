using System;
using System.Collections.Generic;


namespace Lessons.Architecture.PM
{
    public interface IAllStatsPresenter : ISmallPresenter
    {
        HashSet<StatPresenter> StatPresenters { get; }

        event Action StatsUpdateEvent;
        PlayerStat[] GetStats();

    }

}
