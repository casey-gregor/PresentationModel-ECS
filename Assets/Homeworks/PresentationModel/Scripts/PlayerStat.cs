using System;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStat
    {
        public event Action<int> OnValueChanged; 

        public string Name { get; private set; }

        public int Value { get; private set; }

        public PlayerStat(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public void ChangeValue(int value)
        {
            this.Value = value;
            this.OnValueChanged?.Invoke(value);
        }
    }
}