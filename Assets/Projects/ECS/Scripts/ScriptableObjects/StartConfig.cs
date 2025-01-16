using UnityEngine;

namespace ECSProject
{
    [CreateAssetMenu(fileName = "newStartConfig", menuName = "StartConfig/New Start Config", order = 0)]
    public class StartConfig : ScriptableObject
    {
        public UnitsParams[] units;
    }
}