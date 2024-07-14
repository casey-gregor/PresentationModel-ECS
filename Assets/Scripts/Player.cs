using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{

    public class Player : IPresenter, IDisposable
    {
        public event Action OnExperienceChanged;
        public event Action OnLevelUp;

        public PlayerConfig Config => config;
        public Sprite Icon { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public IReadOnlyReactiveProperty<int> Level => level;
        public IReadOnlyReactiveProperty<int> CurrentExperience => currentExperience;  
        public IReadOnlyReactiveProperty<int> RequiredExperience => requiredExperience;
        public bool CanLevelUp { get; private set; }


        private readonly ReactiveProperty<int> requiredExperience = new();
        private readonly ReactiveProperty<int> level = new();
        private readonly ReactiveProperty<int> currentExperience = new();
        private readonly CompositeDisposable compositeDisposable = new();

        private UserInfo userInfo;
        private PlayerLevel playerLevel;
        private StatsInfo stats;

        private PlayerStat moveSpeed;
        private PlayerStat stamina;
        private PlayerStat dexterity;
        private PlayerStat intelligence;
        private PlayerStat damage;
        private PlayerStat regeneration;

        private PlayerConfig config;
        private DiContainer diContainer;

        public Player(PlayerConfig config, DiContainer container)
        {
            this.config = config;
            this.diContainer = container;

            SetupPlayerInfo(config);

            SetupPlayerLevel(config);

            SetupPlayerStats(config);
        }

        public void AddExperience(int value)
        {
            this.playerLevel.AddExperience(value);
        }

       
        private void SetupPlayerInfo(PlayerConfig config)
        {
            this.userInfo = new UserInfo();

            this.userInfo.ChangeName(config.playerName);
            this.Name = this.userInfo.Name;

            this.userInfo.ChangeDescription(config.playerDescription);
            this.Description = this.userInfo.Description;

            this.userInfo.ChangeIcon(config.playerAvatar);
            this.Icon = this.userInfo.Icon;
        }

        private void SetupPlayerLevel(PlayerConfig config)
        {
            this.playerLevel = this.diContainer.Instantiate<PlayerLevel>();
            AddExperience(config.playerExperience);
            this.playerLevel.CurrentExperience.Subscribe(HandleExperienceChanged).AddTo(this.compositeDisposable);
            this.playerLevel.RequiredExperience.Subscribe(HandleRequiredExpChanged).AddTo(this.compositeDisposable);
            this.playerLevel.CurrentLevel.Subscribe(HandleLevelChanged).AddTo(this.compositeDisposable);
        }

        private void HandleRequiredExpChanged(int  value)
        {
            this.requiredExperience.Value = value;
        }
       
        private void HandleExperienceChanged(int value)
        {
            CheckCanLevelUp();
            
            this.currentExperience.Value = value;
        }

        private void HandleLevelChanged(int value)
        {
            this.level.Value = value;
        }

        private void CheckCanLevelUp()
        {
            this.CanLevelUp = this.playerLevel.CanLevelUp();
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

        public PlayerStat[] GetStats()
        {
            return this.stats.GetStats();
        }
        public void LevelUp()
        {
            this.playerLevel.LevelUp();
        }

        public void Dispose()
        {
            this.compositeDisposable.Dispose();
        }
       
    }

}
