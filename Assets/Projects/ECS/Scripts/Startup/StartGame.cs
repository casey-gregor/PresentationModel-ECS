using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ECSProject
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private AddRemoveIcons iconsManagementScript;
        [SerializeField] private UnitsManager unitsManager;
        [SerializeField] private GameObject miniMap;
        
        private Button _button;

        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();
            _button.onClick.AddListener(InitiateGame);
        }

        private List<UnitsParams> PrepareGameConfig()
        {
            List<UnitsParams> unitsParams = new List<UnitsParams>();
            
            List<GameObject> redArchers = iconsManagementScript.GetRedArcherList();
            foreach (GameObject archer in redArchers)
            {
                UnitsParams redArcher = new UnitsParams(UnitTypes.Archer, Teams.Red, archer.transform);
                unitsParams.Add(redArcher);
                Destroy(archer);
            }
            
            List<GameObject> blueArchers = iconsManagementScript.GetBlueArcherList();
            foreach (var archer in blueArchers)
            {
                UnitsParams blueArcher = new UnitsParams(UnitTypes.Archer, Teams.Blue, archer.transform);
                unitsParams.Add(blueArcher);
                Destroy(archer);
            }
            
            List<GameObject> redSwordmen = iconsManagementScript.GetRedSwordsmanList();
            foreach (var swordsman in redSwordmen)
            {
                UnitsParams redSwordsman = new UnitsParams(UnitTypes.Swordsman, Teams.Red, swordsman.transform);
                unitsParams.Add(redSwordsman);
                Destroy(swordsman);
            }
            
            List<GameObject> blueSwordmen = iconsManagementScript.GetBlueSwordsmanList();
            foreach (var swordsman in blueSwordmen)
            {
                UnitsParams blueSwordsman = new UnitsParams(UnitTypes.Swordsman, Teams.Blue, swordsman.transform);
                unitsParams.Add(blueSwordsman);
                Destroy(swordsman);
            }
            
            return unitsParams;
        }

        private void InitiateGame()
        {
            if (CreateSpawnEntities(PrepareGameConfig()) == 0)
            {
                Debug.Log("Add units before starting the game.");
                return;
            }
            
            miniMap.SetActive(false);
            iconsManagementScript.gameObject.SetActive(false);
            
        }

        private int CreateSpawnEntities(List<UnitsParams> unitsParams)
        {
            unitsManager.CreateSpawnEntities(unitsParams);

            return unitsParams.Count;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}