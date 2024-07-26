using System;


namespace Lessons.Architecture.PM
{
    public class StatPresenter : IStatPresenter, IDisposable
    {
        public string Name => name;
        private string name;

        public int Value => value; 
        private int value;

        public PlayerStat PlayerStat => playerStat;
        private PlayerStat playerStat;
        public StatPresenter(string name, int value)
        {
            this.name = name;
            this.value = value;

            this.playerStat = new PlayerStat(this.name, this.value);

            this.playerStat.OnValueChanged += HandleValueChangeEvent;
        }

        private void HandleValueChangeEvent(int newValue)
        {
            this.value = newValue;
        }

        public void Dispose()
        {
            this.playerStat.OnValueChanged -= HandleValueChangeEvent;
        }

        public string GetStatText()
        {
            string text = $"{Name} : {Value}";
            return text;
        }
    }
}