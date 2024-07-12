using UnityEngine;
using UnityEngine.UI;

public enum ButtonState
{
    Active,
    Inactive
}

public class LevelUpButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image levelUpButtonBackground;
    [SerializeField] private Sprite buttonActiveSprite;
    [SerializeField] private Sprite buttonInactiveSprite;
    [SerializeField] private ButtonState state;

    public Button Button { get { return button; } }

    public void SetState(ButtonState state)
    {
        this.state = state;

        switch (state)
        {
            case ButtonState.Active:
                button.interactable = true;
                SetButtonSprite(buttonActiveSprite);
                break;
            case ButtonState.Inactive:
                button.interactable = false;
                SetButtonSprite(buttonInactiveSprite);
                break;
        }
    }

    private void SetButtonSprite(Sprite buttonSprite)
    {
        levelUpButtonBackground.sprite = buttonSprite;
    }
}
