using System;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

namespace ECSHomework
{
    [Serializable]
    public class UnitsParams
    {
        public Entity unitPrefab;
        public UnitTypes type;
        public Teams team;
        public Transform spawnPoint;

    }
}