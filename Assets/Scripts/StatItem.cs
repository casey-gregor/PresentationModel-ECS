using System;
using System.Text;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public class StatItem
    {
        [SerializeField] private PlayerStatNames name;
        public string Name => EnumNameStringConverter(name);

        public int value;

        private string EnumNameStringConverter(Enum value)
        {
            string enumName = value.ToString();

            StringBuilder convertedName = new StringBuilder();

            foreach (char c in enumName)
            {
                if (char.IsUpper(c) && convertedName.Length > 0)
                {
                    convertedName.Append(' ');
                }
                convertedName.Append(c);

            }

            return convertedName.ToString();
        }

    }

}