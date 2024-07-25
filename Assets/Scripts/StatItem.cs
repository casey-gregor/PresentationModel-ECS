using System;
using System.Text;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public class StatItem
    {
        public PlayerStatNames name;
        public int value;

        public string Name => EnumNameStringConverter(name);

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