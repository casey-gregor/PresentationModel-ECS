using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ECSHomework
{
    public class UserGuideController : MonoBehaviour
    {
        [SerializeField] private Button closeBtn;

        private void Awake()
        {
            closeBtn.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            closeBtn.onClick.RemoveAllListeners();
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }
    }
    
}
