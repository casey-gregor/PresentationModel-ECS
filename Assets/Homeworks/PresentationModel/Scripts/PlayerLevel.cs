using System;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevel
    {
        public event Action OnLevelUp;
        public event Action<int> OnExperienceChanged;

        public int CurrentLevel { get; private set; } = 1;

        public int CurrentExperience { get; private set; }

        public int RequiredExperience
        {
            get { return 100 * (this.CurrentLevel + 1); }
        }

        private int unusedExperience;

        public void AddExperience(int value)
        {
            int newTotalXP = this.CurrentExperience + value;

            unusedExperience = Mathf.Abs(this.RequiredExperience - newTotalXP);
            Debug.Log("unusedxp : " + unusedExperience);

            var xpToAdd = Math.Min(newTotalXP, this.RequiredExperience);
            Debug.Log("xp to add : " + xpToAdd);

            this.CurrentExperience = xpToAdd;
            this.OnExperienceChanged?.Invoke(xpToAdd);

        }

        public void LevelUp()
        {
            if (this.CanLevelUp())
            {
                this.CurrentExperience = 0;
                this.CurrentLevel++;
                AddUnusedExperience();
                this.OnLevelUp?.Invoke();
            }
        }

        public bool CanLevelUp()
        {
            return this.CurrentExperience == this.RequiredExperience;
        }

        private void AddUnusedExperience()
        {
            if(unusedExperience > 0)
            {
                AddExperience(unusedExperience);
            }
        }
    }
}