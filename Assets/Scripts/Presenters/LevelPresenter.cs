using System;
using UniRx;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class LevelPresenter : ILevelPresenter, IDisposable
    {
        public bool CanLevelUp { get; private set; }
        public IReadOnlyReactiveProperty<int> Level => level;
        public IReadOnlyReactiveProperty<int> CurrentExperience => currentExperience;
        public IReadOnlyReactiveProperty<int> RequiredExperience => requiredExperience;

        private readonly ReactiveProperty<int> level = new();
        private readonly ReactiveProperty<int> requiredExperience = new();
        private readonly ReactiveProperty<int> currentExperience = new();

        private PlayerLevel playerLevel;

        private readonly CompositeDisposable compositeDisposable = new();
        public LevelPresenter(PlayerConfig config, DiContainer diContainer)
        {
            SetupPlayerLevel(config, diContainer);
        }

        private void SetupPlayerLevel(PlayerConfig config, DiContainer diContainer)
        {
            this.compositeDisposable.Clear();
            this.playerLevel = diContainer.Instantiate<PlayerLevel>();
            AddExperience(config.playerExperience);
            this.playerLevel.CurrentExperience.Subscribe(HandleExperienceChanged).AddTo(this.compositeDisposable);
            this.playerLevel.RequiredExperience.Subscribe(HandleRequiredExpChanged).AddTo(this.compositeDisposable);
            this.playerLevel.CurrentLevel.Subscribe(HandleLevelChanged).AddTo(this.compositeDisposable);
        }

        public void AddExperience(int value)
        {
            this.playerLevel.AddExperience(value);
        }

        private void HandleRequiredExpChanged(int value)
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

        public void LevelUp()
        {
            this.playerLevel.LevelUp();
        }

        public void Dispose()
        {
            this.compositeDisposable.Dispose();
        }

        public string GetLevelText()
        {
            string text = $"Level: {level}";
            return text;
        }

        public string GetXPText()
        {
            string text = $"XP: {currentExperience}/{requiredExperience}";
            return text;
        }
    }
}