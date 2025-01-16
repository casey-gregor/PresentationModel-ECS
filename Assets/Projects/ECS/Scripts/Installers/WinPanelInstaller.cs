using TMPro;
using UnityEngine;

namespace ECSProject
{
    public class WinPanelInstaller : ComponentsInstaller
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip winSound;
        [SerializeField] private GameObject winPanel;

        public override void Install()
        {
            Entity.SetData(new AudioSourceComponent { Value = audioSource });
            Entity.SetData(new WinSFX { Value = winSound });
            Entity.SetData(new GameObjectComponent { Value = winPanel });
        }
    }
}
