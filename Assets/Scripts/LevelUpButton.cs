using UnityEngine;
using UnityEngine.UI;

public enum ButtonState
{
    Active,
    Inactive
}

public class LevelUpButton : MonoBehaviour
{
    public Button Button => button;
    
    [SerializeField] private Button button;
    [SerializeField] private Image levelUpButtonBackground;
    [SerializeField] private Sprite buttonActiveSprite;
    [SerializeField] private Sprite buttonInactiveSprite;

    public void SetState(ButtonState state)
    {

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
