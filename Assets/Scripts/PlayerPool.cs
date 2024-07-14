using System.Collections.Generic;
using System.Linq;

namespace Lessons.Architecture.PM
{
    public class PlayerPool
    {
        private HashSet<Player> players;
        public PlayerPool()
        {
            this.players = new HashSet<Player>();
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public Player GetPlayer(PlayerConfig config)
        {
            
            return players.FirstOrDefault(player => player.Config == config);
        }
        public bool HasPlayer(PlayerConfig playerConfig)
        {
            return players.Any(player => player.Config == playerConfig);
        }
    }
}

