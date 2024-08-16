using System;
using System.Collections.Generic;
using System.Linq;

namespace Lessons.Architecture.PM
{
    public sealed class StatsInfo
    {
        public event Action<PlayerStat> OnStatAdded;
        public event Action<PlayerStat> OnStatRemoved;
    
        private readonly HashSet<PlayerStat> Stats = new();

        public void AddStat(PlayerStat stat)
        {
            if (this.Stats.Add(stat))
            {
                this.OnStatAdded?.Invoke(stat);
            }
        }

        public void RemoveStat(PlayerStat stat)
        {
            if (this.Stats.Remove(stat))
            {
                this.OnStatRemoved?.Invoke(stat);
            }
        }

        public PlayerStat GetStat(string name)
        {
            foreach (var stat in this.Stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }
            return null;
        }

        public PlayerStat[] GetStats()
        {
            return this.Stats.ToArray();
        }
    }
}