using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OutsideButton : MonoBehaviour
{
    public event Action CloseEvent;

    private Button button;

    private void Awake()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(HandleOnClickEvent);
    }
    private void HandleOnClickEvent()
    {
        this.CloseEvent?.Invoke();
    }

    private void OnDestroy()
    {
        this.button.onClick.RemoveListener(HandleOnClickEvent);
    }
}
