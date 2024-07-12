using Lessons.Architecture.PM;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerConfig", menuName ="Configs/New PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public string playerName;
    [TextArea(3, 5)] public string playerDescription;
    public Sprite playerAvatar;

    public int playerExperience = 0;

}
