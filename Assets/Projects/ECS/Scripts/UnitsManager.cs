using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ECSHomework
{
    public class UnitsManager : MonoBehaviour
    {
        [SerializeField] private int numOfGroupsOnStart = 1;
        
        [SerializeField] private List<UnitConfig> redTeamUnitConfigs;
        // [SerializeField] private List<UnitConfig> blueTeamUnitConfigs;
        
        [SerializeField] private Transform redTeamUnitsContainer;
        [SerializeField] private Transform blueTeamUnitsContainer;
        
        [SerializeField] private Transform world;
        
        [SerializeField] private Transform[] redTeamSpawnPoints;
        
        private TeamManager redTeamManager;
        // private TeamManager blueTeamManager;

        private void Awake()
        {
            redTeamManager = new TeamManager(redTeamUnitConfigs, world);
            // blueTeamManager = new TeamManager(blueTeamUnitConfigs, world);

            for (int i = 0; i < numOfGroupsOnStart; i++)
            {
                redTeamManager.SpawnUnit(
                    UnitType.Archer, 
                    redTeamSpawnPoints[0].position, 
                    redTeamSpawnPoints[0].rotation);
                
                redTeamManager.SpawnUnit(
                    UnitType.Swordsman, 
                    redTeamSpawnPoints[1].position, 
                    redTeamSpawnPoints[1].rotation);
            }
        }
        
    }
}