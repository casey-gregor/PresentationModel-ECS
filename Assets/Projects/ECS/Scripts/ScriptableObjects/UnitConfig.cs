using UnityEngine;

namespace ECSHomework
{
    [CreateAssetMenu(fileName = "NewUnit", menuName = "Units/New Unit", order = 0)]
    public class UnitConfig : ScriptableObject
    {
        public Teams team;
        public UnitType unitType;
        public Entity Prefab;
        public int NumOfUnits;
        public Transform PoolParent;
    }
}