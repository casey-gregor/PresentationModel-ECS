using Lessons.Architecture.PM;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PopupView : MonoBehaviour
{
    public IPresenter presenter { get; private set; }
    
    [SerializeField] private Image heroAva;
    [SerializeField] private TextMeshProUGUI heroName;
    [SerializeField] private TextMeshProUGUI heroDescription;
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private Slider expericeSlider;
    [SerializeField] private LevelUpButton levelUpButton;
    [SerializeField] private OutsideButton closeButton;

    [SerializeField] private StatSlot[] statSlots;

    private PlayerStat[] existingStats;

    private CompositeDisposable compositeDisposable;


    public void ShowPopup(IPresenter presenter)
    {
        this.presenter = presenter;

        if (this.gameObject.activeSelf == false)
        {
            this.gameObject.SetActive(true);
            this.compositeDisposable = new CompositeDisposable();
        }

        UpdateUserInfo();
        UpdateLevelText();
        UpdateXPtext();
        UpdateSliderValues();

        this.existingStats = this.presenter.GetStats();

        for (int i = 0; i < this.statSlots.Length; i++)
        {
            if (i < this.existingStats.Length)
            {
                UpdateStatsText(this.statSlots[i], this.existingStats[i]);
            }
            else
            {
                this.statSlots[i].StatObject.SetActive(false);
            }
        }

        this.presenter.CurrentExperience.Subscribe(HandleXPUpdateEvent).AddTo(compositeDisposable);
        this.presenter.Level.Subscribe(HandleLevelUpdateEvent).AddTo(compositeDisposable);
        this.levelUpButton.Button.onClick.AddListener(this.presenter.LevelUp);
        this.closeButton.closeEvent += HidePopup;

        SetLevelUpButtonState();
    }

    private void UpdateUserInfo()
    {
        this.heroAva.sprite = this.presenter.Icon;
        this.heroName.text = this.presenter.Name;
        this.heroDescription.text = this.presenter.Description;
    }
    private void UpdateLevelText()
    {
        int level = this.presenter.Level.Value;
        this.levelNumber.text = $"Level: {level}";
    }

    private void UpdateXPtext()
    {
        int currentXP = this.presenter.CurrentExperience.Value;
        int requiredXP = this.presenter.RequiredExperience.Value;
        this.experienceText.text = $"XP: {currentXP}/{requiredXP}";
    }

    private void SetLevelUpButtonState()
    {
        ButtonState state = this.presenter.CanLevelUp ? ButtonState.Active : ButtonState.Inactive;
        this.levelUpButton.SetState(state);
    }

  
    public void HidePopup()
    {
        this.gameObject.SetActive(false);
    }
   
    
    private void UpdateSliderValues()
    {
        this.expericeSlider.maxValue = this.presenter.RequiredExperience.Value;
        this.expericeSlider.value = this.presenter.CurrentExperience.Value;
    }

    private void UpdateStats()
    {
        for(int i = 0; i < this.existingStats.Length; i++)
        {
            UpdateStatsText(this.statSlots[i], this.existingStats[i]);
        }
    }

    private void UpdateStatsText(StatSlot statTo, PlayerStat statFrom)
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

    private void HandleLevelUpdateEvent(int _)
    {
        UpdateLevelText();
        SetLevelUpButtonState();
        UpdateXPtext();
        UpdateSliderValues();
        UpdateStats();
    }

    private void OnDisable()
    {
        this.compositeDisposable.Dispose();
        this.levelUpButton.Button.onClick.RemoveListener(this.presenter.LevelUp);
        this.closeButton.closeEvent -= HidePopup;
    }
}
