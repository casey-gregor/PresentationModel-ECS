using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class AllStatsPresenter : IAllStatsPresenter
    {
        public event Action StatsUpdateEvent;

        private StatsInfo playerStats;

        public HashSet<StatPresenter> StatPresenters => statPresenters;
        private HashSet<StatPresenter> statPresenters = new();

        public AllStatsPresenter(PlayerConfig playerConfig, DiContainer diContainer)
        {
            SetupPlayerStats(playerConfig, diContainer);
        }

        private void SetupPlayerStats(PlayerConfig config, DiContainer diContainer)
        {
            this.playerStats = new StatsInfo();

            foreach (StatItem statItem in config.statsArray)
            {
                StatPresenter statPresenter = diContainer.Instantiate<StatPresenter>(new object[] { statItem.Name, statItem.value });

                this.statPresenters.Add(statPresenter);
                this.playerStats.AddStat(statPresenter.PlayerStat);
            }

        }

        public PlayerStat[] GetStats()
        {
            return this.playerStats.GetStats();
        }

        public void UpdateStats(PlayerStat[] currentStats, int statToAdd)
        {

            foreach (PlayerStat stat in currentStats)
            {
                //Debug.Log("updating stat for : " + stat.Name);
                stat.ChangeValue(stat.Value + statToAdd);
            }
            
            StatsUpdateEvent?.Invoke();
        }
    }
}