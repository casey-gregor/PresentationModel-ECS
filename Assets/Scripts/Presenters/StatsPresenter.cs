using System;

namespace Lessons.Architecture.PM
{
    public class StatsPresenter : IStatsPresenter
    {
        public event Action StatsUpdateEvent;

        private StatsInfo stats;

        public StatsPresenter(PlayerConfig playerConfig)
        {
            SetupPlayerStats(playerConfig);
        }

        private void SetupPlayerStats(PlayerConfig config)
        {
            this.stats = new StatsInfo();

            foreach (StatItem statItem in config.statsArray)
            {

                PlayerStat newStat = new PlayerStat(statItem.Name, statItem.value);

                this.stats.AddStat(newStat);
            }

        }

        public PlayerStat[] GetStats()
        {
            return this.stats.GetStats();
        }

        public void UpdateStats(PlayerStat[] currentStats, int statToAdd)
        {

            foreach (PlayerStat stat in currentStats)
            {
                stat.ChangeValue(stat.Value + statToAdd);
            }

            StatsUpdateEvent?.Invoke();
        }
    }
}