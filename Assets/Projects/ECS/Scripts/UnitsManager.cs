using System;
using System.Collections.Generic;
using System.Linq;
using ECSHomework.UnitManagerComponents;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

namespace ECSHomework
{
    public class UnitsManager : MonoBehaviour
    {
        [SerializeField] private StartConfig startConfig;
        
        [SerializeField] private List<UnitConfig> redTeamUnitConfigs;
        [SerializeField] private List<UnitConfig> blueTeamUnitConfigs;
        
        [SerializeField] private Transform world;
        [SerializeField] private Transform unitsPoolParent;

        [SerializeField] private Teams redTeam;
        [SerializeField] private Teams blueTeam;
        
        [SerializeField] private Transform[] redTeamSpawnPoints;
        [SerializeField] private Transform[] blueTeamSpawnPoints;
        

        private EcsStartup _ecsStartUp;
        
        [Inject]
        public void Construct(EcsStartup ecsStartup)
        {
            _ecsStartUp = ecsStartup;
        }

        private void Start()
        {
            _ecsStartUp.CreateEntity()
                .AddComponent(new CreateUnitPools())
                .AddComponent(new UnitConfigList { Value = redTeamUnitConfigs })
                .AddComponent(new WorldParent { Value = world })
                .AddComponent(new UnitsParent { Value = unitsPoolParent })
                .AddComponent(new TeamComponent { Value = redTeam })
                .AddComponent(new StartConfigComponent { Value = startConfig });

            _ecsStartUp.CreateEntity()
                .AddComponent(new CreateUnitPools())
                .AddComponent(new UnitConfigList { Value = blueTeamUnitConfigs })
                .AddComponent(new WorldParent { Value = world })
                .AddComponent(new UnitsParent { Value = unitsPoolParent })
                .AddComponent(new TeamComponent { Value = blueTeam })
                .AddComponent(new StartConfigComponent { Value = startConfig });
        }
    }
}