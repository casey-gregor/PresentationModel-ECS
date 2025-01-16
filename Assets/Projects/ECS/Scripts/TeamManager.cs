using System.Collections.Generic;
using UnityEngine;

namespace ECSProject
{
    public class TeamManager
    {
        private readonly Dictionary<UnitTypes, Pool> _unitPools = new();
        
        public TeamManager(List<UnitConfig> unitConfigs, Transform world, Transform poolParent)
        {
            
            foreach (var unitConfig in unitConfigs)
            {
                Pool pool = new Pool(unitConfig, world, poolParent);

                if (!_unitPools.ContainsKey(unitConfig.type))
                {
                    _unitPools.Add(unitConfig.type, pool);
                }
            }
        }

        public Entity SpawnUnit(UnitTypes type, Vector3 position, Quaternion rotation)
        {
            if (_unitPools.TryGetValue(type, out var pool))
            {
                Entity unit = pool.GetObject();
                unit.transform.position = position;
                unit.transform.rotation = rotation;
                
                return unit;
            }
        
            return null;
        }

        public void ReturnUnit(UnitTypes type, Entity unit)
        {
            if (_unitPools.ContainsKey(type))
            {
                _unitPools[type].Enqueue(unit);
            }
        }
    }
}