using Lessons.Architecture.PM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupView : MonoBehaviour
{
    [SerializeField] private Image heroAva;
    [SerializeField] private TextMeshProUGUI heroName;
    [SerializeField] private TextMeshProUGUI heroDescription;
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private Slider expericeSlider;
    [SerializeField] private LevelUpButton levelUpButton;
    [SerializeField] private OutsideButton closeButton;

    [SerializeField] private Stat[] stats;

    private PlayerStat[] existingStats;

    public IPresenter presenter { get; private set; }

    private void Awake()
    {
        closeButton.closeEvent += HidePopup;
    }

    public void ShowPopup(IPresenter presenter)
    {
        gameObject.SetActive(true);

        this.presenter = presenter;

        this.heroAva.sprite = this.presenter.UserInfo.Icon;
        this.heroName.text = this.presenter.UserInfo.Name;
        this.heroDescription.text = this.presenter.UserInfo.Description;

        UpdateLevelText();
        UpdateXPtext();
        UpdateSliderValues();

        this.presenter.PlayerLevel.OnExperienceChanged += HandleXPUpdateEvent;
        this.presenter.PlayerLevel.OnLevelUp += HandleLevelUpdateEvent;

        existingStats = this.presenter.Stats.GetStats();

        for(int i = 0; i < existingStats.Length; i++)
        {
            if (stats[i] != null)
            {
                UpdateStatsText(stats[i], existingStats[i]);
            }
        }

        this.levelUpButton.Button.onClick.AddListener(this.presenter.PlayerLevel.LevelUp); ;
        SetLevelUpButtonState();
    }
    private void UpdateLevelText()
    {
        int level = this.presenter.PlayerLevel.CurrentLevel;
        this.levelNumber.text = $"Level: {level}";
    }

    private void UpdateXPtext()
    {
        int currentXP = this.presenter.PlayerLevel.CurrentExperience;
        int requiredXP = this.presenter.PlayerLevel.RequiredExperience;
        this.experienceText.text = $"XP: {currentXP}/{requiredXP}";
    }

    private void SetLevelUpButtonState()
    {
        ButtonState state = this.presenter.PlayerLevel.CanLevelUp() ? ButtonState.Active : ButtonState.Inactive;
        this.levelUpButton.SetState(state);
    }

  
    public void HidePopup()
    {
        gameObject.SetActive(false);
    }
   
    
    private void UpdateSliderValues()
    {
        this.expericeSlider.maxValue = this.presenter.PlayerLevel.RequiredExperience;
        this.expericeSlider.value = this.presenter.PlayerLevel.CurrentExperience;
    }

    private void UpdateStats()
    {
        for(int i = 0; i < existingStats.Length; i++)
        {
            UpdateStatsText(stats[i], existingStats[i]);
        }
    }

    private void UpdateStatsText(Stat statTo, PlayerStat statFrom)
    {
        TextMeshProUGUI textMeshPro = statTo.StatObject.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = $"{statFrom.Name} : {statFrom.Value}";
    }

   

    private void HandleXPUpdateEvent(int value)
    {
        UpdateXPtext();
        UpdateSliderValues();
        SetLevelUpButtonState();
    }

    private void HandleLevelUpdateEvent()
    {
        UpdateLevelText();
        SetLevelUpButtonState();
        UpdateXPtext();
        UpdateSliderValues();
        UpdateStats();
    }
}
