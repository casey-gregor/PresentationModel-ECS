using System.Collections.Generic;
using System.Linq;

namespace Lessons.Architecture.PM
{
    public class PlayerPool
    {
        private HashSet<PlayerPresenter> players;

        public PlayerPool()
        {
            this.players = new HashSet<PlayerPresenter>();
        }

        public void AddPlayer(PlayerPresenter player)
        {
            this.players.Add(player);
        }

        public PlayerPresenter GetPlayer(PlayerConfig config)
        {
            return this.players.FirstOrDefault(player => player.Config == config);
        }

        public bool HasPlayer(PlayerConfig playerConfig)
        {
            return this.players.Any(player => player.Config == playerConfig);
        }
    }
}

