using Lessons.Architecture.PM;
using UnityEngine;

public class Player : IPresenter
{
    public UserInfo UserInfo => userInfo;
    public PlayerLevel PlayerLevel => level;
    public StatsInfo Stats => stats;

    private UserInfo userInfo;
    private PlayerLevel level;
    private StatsInfo stats;

    private PlayerStat moveSpeed;
    private PlayerStat stamina;
    private PlayerStat dexterity;
    private PlayerStat intelligence;
    private PlayerStat damage;
    private PlayerStat regeneration;

    private PlayerConfig playerConfig;
   

    public Player(PlayerConfig config)
    {
        this.playerConfig = config;

        SetupPlayerLevel(config);

        SetupPlayerInfo(config);

        SetupPlayerStats();
    }

    private void SetupPlayerStats()
    {
        this.stats = new StatsInfo();

        this.moveSpeed = new PlayerStat("Move Speed", 0);
        this.stats.AddStat(this.moveSpeed);

        this.stamina = new PlayerStat("Stamina", 0);
        this.stats.AddStat(this.stamina);

        this.dexterity = new PlayerStat("Dexterity", 0);
        this.stats.AddStat(this.dexterity);

        this.intelligence = new PlayerStat("Intelligence", 0);
        this.stats.AddStat(this.intelligence);

        this.damage = new PlayerStat("Damage", 0);
        this.stats.AddStat(this.damage);

        this.regeneration = new PlayerStat("Regeneration", 0);
        this.stats.AddStat(this.regeneration);
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
}
