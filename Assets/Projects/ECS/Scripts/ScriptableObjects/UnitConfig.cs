using UnityEngine;
using UnityEngine.Serialization;

namespace ECSProject
{
    [CreateAssetMenu(fileName = "NewUnitConfig", menuName = "Units/New UnitConfig", order = 0)]
    public class UnitConfig : ScriptableObject
    {
        public Teams team;
        public UnitTypes type;
        public Entity prefab;
    }
}