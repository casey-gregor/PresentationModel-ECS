using System;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

namespace ECSHomework
{
    [Serializable]
    public class UnitsParams
    {
        public UnitTypes type;
        public Teams team;
        public Transform spawnPoint;

        public UnitsParams(UnitTypes type, Teams team, Transform spawnPoint)
        {
            this.type = type;
            this.team = team;
            this.spawnPoint = spawnPoint;
        }

    }
}