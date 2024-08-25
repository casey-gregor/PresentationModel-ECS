using System.Collections.Generic;
using UnityEngine;

namespace ECSHomework
{
    public class TeamManager
    {
        private Transform world;
        private List<UnitConfig> unitConfigs;

        private Dictionary<UnitType, Pool> _unitPools = new();
        
        public TeamManager(List<UnitConfig> unitConfigs, Transform world)
        {
            foreach (var unitConfig in unitConfigs)
            {
                Pool pool = new Pool(unitConfig, world);
                _unitPools.Add(unitConfig.unitType, pool);
            }
        }

        public Entity SpawnUnit(UnitType unitType, Vector3 position, Quaternion rotation)
        {
            if (_unitPools.ContainsKey(unitType))
            {
                Entity unit = _unitPools[unitType].GetObject();
                unit.transform.position = position;
                unit.transform.rotation = rotation;
                
                return unit;
            }

            return null;
        }

        public void ReturnUnit(UnitType unitType, Entity unit)
        {
            if (_unitPools.ContainsKey(unitType))
            {
                _unitPools[unitType].Enqueue(unit);
            }
        }
    }
}