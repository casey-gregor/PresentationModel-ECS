using System;
using UniRx;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevel : IDisposable
    {
        public event Action OnLevelUp;
        public event Action<int> OnExperienceChanged;

        public IReadOnlyReactiveProperty<int> CurrentLevel => currentLevel;
        private readonly ReactiveProperty<int> currentLevel = new();

        public IReadOnlyReactiveProperty<int> CurrentExperience => currentExperience;
        private readonly ReactiveProperty<int> currentExperience = new();

        public IReadOnlyReactiveProperty<int> RequiredExperience => requiredExperience;
        private readonly ReactiveProperty<int> requiredExperience = new();


        private int unusedExperience;
        private IDisposable disposable;

        public PlayerLevel()
        {
            currentLevel.Value = 1;
            this.disposable = this.currentLevel.Subscribe(HandleUpdateOfRequiredExperience);
        }

        private void HandleUpdateOfRequiredExperience(int _)
        {
            this.requiredExperience.Value = 100 *(this.currentLevel.Value + 1);
        }

        public void AddExperience(int value)
        {
            int newTotalXP = this.currentExperience.Value + value;

            this.unusedExperience = Mathf.Abs(this.requiredExperience.Value - newTotalXP);

            var xpToAdd = Math.Min(newTotalXP, this.requiredExperience.Value);

            this.currentExperience.SetValueAndForceNotify(this.currentExperience.Value = xpToAdd);

        }

        public void LevelUp()
        {
            if (this.CanLevelUp())
            {
                this.currentExperience.SetValueAndForceNotify(this.currentExperience.Value = 0);
                this.currentLevel.Value++;
                AddUnusedExperience();
                this.OnLevelUp?.Invoke();
            }
        }

        public bool CanLevelUp()
        {
            return this.CurrentExperience.Value == this.RequiredExperience.Value;
        }

        private void AddUnusedExperience()
        {
            if(this.unusedExperience > 0)
            {
                AddExperience(this.unusedExperience);
            }
        }

        public void Dispose()
        {
            this.disposable.Dispose();
        }
    }
}