using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TMPro;
using UnityEngine;

namespace ECSProject
{
    public class DisplayWinPanel : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventsWorld = EcsWorlds.EVENTS_WORLD;
        
        private readonly EcsFilterInject<Inc<CanDisplayWinText, TeamComponent>> _filterEvent = EcsWorlds.EVENTS_WORLD;

        private readonly EcsFilterInject<Inc<GameObjectComponent>> _gameObjectFilter;
        public void Run(EcsSystems systems)
        {
            EcsPool<TeamComponent> teamComponentPool = _filterEvent.Pools.Inc2;

            foreach (var _event in _filterEvent.Value)
            {
                Teams wonTeam = teamComponentPool.Get(_event).Value;
                
                foreach (int entity in _gameObjectFilter.Value)
                {
                    GameObject winPanel = _gameObjectFilter.Pools.Inc1.Get(entity).Value;
                    winPanel.SetActive(true);
                    
                    TextMeshProUGUI textMesh = winPanel.GetComponentInChildren<TextMeshProUGUI>();
                    
                    string winText = $"Team {wonTeam} has won!";
                    textMesh.text = winText;
                }

                _eventsWorld.Value.DelEntity(_event);
            }
        }
    }
}