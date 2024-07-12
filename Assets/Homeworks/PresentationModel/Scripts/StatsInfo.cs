using System;
using System.Collections.Generic;
using System.Linq;

namespace Lessons.Architecture.PM
{
    public sealed class StatsInfo
    {
        public event Action<PlayerStat> OnStatAdded;
        public event Action<PlayerStat> OnStatRemoved;
    
        private readonly HashSet<PlayerStat> stats = new();

        public void AddStat(PlayerStat stat)
        {
            if (this.stats.Add(stat))
            {
                this.OnStatAdded?.Invoke(stat);
            }
        }

        public void RemoveStat(PlayerStat stat)
        {
            if (this.stats.Remove(stat))
            {
                this.OnStatRemoved?.Invoke(stat);
            }
        }

        public PlayerStat GetStat(string name)
        {
            foreach (var stat in this.stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public PlayerStat[] GetStats()
        {
            return this.stats.ToArray();
        }
    }
}