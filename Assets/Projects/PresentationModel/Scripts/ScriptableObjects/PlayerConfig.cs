using UnityEngine;


namespace Lessons.Architecture.PM
{

    // [CreateAssetMenu(fileName ="PlayerConfig", menuName ="Configs/New PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public string playerName;
        [TextArea(3, 5)] public string playerDescription;
        public Sprite playerAvatar;

        [Space(10)]
        public int playerExperience = 0;

        [Space(10)]
        public StatItem[] statsArray;
    }
}
