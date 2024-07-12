using System;
using UnityEngine;
using UnityEngine.UI;

public class OutsideButton : MonoBehaviour
{
    public event Action closeEvent;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(HandleOnClickEvent);
    }
    private void HandleOnClickEvent()
    {
        closeEvent?.Invoke();
    }
}
