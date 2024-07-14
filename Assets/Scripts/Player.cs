

namespace Lessons.Architecture.PM
{

    public class Player : IPresenter
    {
        public UserInfo UserInfo => userInfo;
        public PlayerLevel PlayerLevel => level;
        public StatsInfo Stats => stats;
        public PlayerConfig Config => config;

        private UserInfo userInfo;
        private PlayerLevel level;
        private StatsInfo stats;

        private PlayerStat moveSpeed;
        private PlayerStat stamina;
        private PlayerStat dexterity;
        private PlayerStat intelligence;
        private PlayerStat damage;
        private PlayerStat regeneration;

        private PlayerConfig config;
   

        public Player(PlayerConfig config)
        {
            this.config = config;

            SetupPlayerInfo(config);

            SetupPlayerLevel(config);

            SetupPlayerStats(config);
        }
        private void SetupPlayerInfo(PlayerConfig config)
        {
            this.userInfo = new UserInfo();
            this.userInfo.ChangeName(config.playerName);
            this.userInfo.ChangeDescription(config.playerDescription);
            this.userInfo.ChangeIcon(config.playerAvatar);
        }

        private void SetupPlayerLevel(PlayerConfig config)
        {
            this.level = new PlayerLevel();
            this.level.AddExperience(config.playerExperience);
        }

        private void SetupPlayerStats(PlayerConfig config)
        {
            this.stats = new StatsInfo();

            this.moveSpeed = new PlayerStat(PlayerStatNames.moveSpeed, config.moveSpeed);
            this.stats.AddStat(this.moveSpeed);

            this.stamina = new PlayerStat(PlayerStatNames.stamina, config.stamina);
            this.stats.AddStat(this.stamina);

            this.dexterity = new PlayerStat(PlayerStatNames.dexterity, config.dexterity);
            this.stats.AddStat(this.dexterity);

            this.intelligence = new PlayerStat(PlayerStatNames.intelligence, config.intelligence);
            this.stats.AddStat(this.intelligence);

            this.damage = new PlayerStat(PlayerStatNames.damage, config.damage);
            this.stats.AddStat(this.damage);

            this.regeneration = new PlayerStat(PlayerStatNames.regeneration, config.regeneration);
            this.stats.AddStat(this.regeneration);
        }

    
    }

}
