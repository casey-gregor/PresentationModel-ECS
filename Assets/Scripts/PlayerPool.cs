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
            this.players.Add(player);
        }

        public Player GetPlayer(PlayerConfig config)
        {
            
            return this.players.FirstOrDefault(player => player.Config == config);
        }
        public bool HasPlayer(PlayerConfig playerConfig)
        {
            return this.players.Any(player => player.Config == playerConfig);
        }
    }
}

