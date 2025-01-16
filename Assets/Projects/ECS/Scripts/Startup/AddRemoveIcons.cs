using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace ECSProject
{
    public class AddRemoveIcons : MonoBehaviour
    {
        [SerializeField] private GameObject redArcherIconPrefab;
        [SerializeField] private GameObject redSwordsmanIconPrefab;
        [SerializeField] private GameObject blueArcherIconPrefab;
        [SerializeField] private GameObject blueSwordsmanIconPrefab;

        [SerializeField] private Button addButton;
        [SerializeField] private Button removeButton;

        [SerializeField] private Transform redSpawnPoint;
        [SerializeField] private Transform blueSpawnPoint;
        
        [SerializeField] private Toggle archerToggle;
        [SerializeField] private Toggle swordsmanToggle;

        [SerializeField] private TextMeshProUGUI archerCount;
        [SerializeField] private TextMeshProUGUI swordsmanCount;

        private readonly List<GameObject> _redArcherIcons = new();
        private readonly List<GameObject> _redSwordsmanIcons = new();
        
        private readonly List<GameObject> _blueArcherIcons = new();
        private readonly List<GameObject> _blueSwordsmanIcons = new();
        
        private DisplayIconsCount _displayIconsCount;

        private void Awake()
        {
            addButton.onClick.AddListener(AddSoldierIcon);
            removeButton.onClick.AddListener(RemoveSoldierIcon);

            _displayIconsCount = new DisplayIconsCount(archerCount, swordsmanCount);
        }

        private void AddSoldierIcon()
        {
            if (archerToggle.isOn)
            {
                GameObject redArcher = Instantiate(redArcherIconPrefab, redSpawnPoint.position, redSpawnPoint.rotation);
                GameObject blueArcher = Instantiate(blueArcherIconPrefab, blueSpawnPoint.position, blueSpawnPoint.rotation);
                
                _redArcherIcons.Add(redArcher);
                _blueArcherIcons.Add(blueArcher);
            }
            else if (swordsmanToggle.isOn)
            {
                GameObject redSwordsman = Instantiate(redSwordsmanIconPrefab, redSpawnPoint.position, redSpawnPoint.rotation);
                GameObject blueSwordsman = Instantiate(blueSwordsmanIconPrefab, blueSpawnPoint.position, blueSpawnPoint.rotation);
                
                _redSwordsmanIcons.Add(redSwordsman);
                _blueSwordsmanIcons.Add(blueSwordsman);
            }
        }

        private void RemoveSoldierIcon()
        {
            if (archerToggle.isOn)
            {
                if (_redArcherIcons.Count > 0 && _blueArcherIcons.Count > 0)
                {
                    Destroy(_redArcherIcons[_redArcherIcons.Count - 1]);
                    _redArcherIcons.RemoveAt(_redArcherIcons.Count - 1);
                    
                    Destroy(_blueArcherIcons[_blueArcherIcons.Count - 1]);
                    _blueArcherIcons.RemoveAt(_blueArcherIcons.Count - 1);
                    
                }
            }
            else if (swordsmanToggle.isOn)
            {
                if (_redSwordsmanIcons.Count > 0 && _blueSwordsmanIcons.Count > 0)
                {
                    Destroy(_redSwordsmanIcons[_redSwordsmanIcons.Count - 1]);
                    _redSwordsmanIcons.RemoveAt(_redSwordsmanIcons.Count - 1);
                    
                    Destroy((_blueSwordsmanIcons[_blueSwordsmanIcons.Count - 1]));
                    _blueSwordsmanIcons.RemoveAt(_blueSwordsmanIcons.Count - 1);
                    
                }
            }
        }

        private void Update()
        {
            int archerCount = _redArcherIcons.Count;
            int swordsmanCount = _redSwordsmanIcons.Count;
            _displayIconsCount.DisplayCount(archerCount, swordsmanCount);
        }

        public List<GameObject> GetRedArcherList()
        {
            return _redArcherIcons;
        }
        public List<GameObject> GetBlueArcherList()
        {
            return _blueArcherIcons;
        }
        
        public List<GameObject> GetRedSwordsmanList()
        {
            return _redSwordsmanIcons;
        }
        public List<GameObject> GetBlueSwordsmanList()
        {
            return _blueSwordsmanIcons;
        }
    }
}