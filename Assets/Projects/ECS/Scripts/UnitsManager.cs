using System.Collections.Generic;
using ECSProject.UnitManagerComponents;
using UnityEngine;
using Zenject;

namespace ECSProject
{
    public class UnitsManager : MonoBehaviour
    {
        
        [SerializeField] private List<UnitConfig> redTeamUnitConfigs;
        [SerializeField] private List<UnitConfig> blueTeamUnitConfigs;
        
        [SerializeField] private Transform world;
        [SerializeField] private Transform unitsPoolParent;

        [SerializeField] private Teams redTeam;
        [SerializeField] private Teams blueTeam;
        
        private EcsStartup _ecsStartUp;
        
        [Inject]
        public void Construct(EcsStartup ecsStartup)
        {
            _ecsStartUp = ecsStartup;
        }
        
        public void CreateSpawnEntities(List<UnitsParams> config)
        {
            _ecsStartUp.CreateEntity()
                .AddComponent(new CreateUnitPools())
                .AddComponent(new UnitConfigList { Value = redTeamUnitConfigs })
                .AddComponent(new WorldParent { Value = world })
                .AddComponent(new UnitsParent { Value = unitsPoolParent })
                .AddComponent(new TeamComponent { Value = redTeam })
                .AddComponent(new StartConfigComponent { Value = config });

            _ecsStartUp.CreateEntity()
                .AddComponent(new CreateUnitPools())
                .AddComponent(new UnitConfigList { Value = blueTeamUnitConfigs })
                .AddComponent(new WorldParent { Value = world })
                .AddComponent(new UnitsParent { Value = unitsPoolParent })
                .AddComponent(new TeamComponent { Value = blueTeam })
                .AddComponent(new StartConfigComponent { Value = config });
        }
    }
}