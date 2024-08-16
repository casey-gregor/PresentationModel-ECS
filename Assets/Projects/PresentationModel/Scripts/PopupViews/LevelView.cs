using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public class LevelView : MonoBehaviour, IViewable
    {
        [SerializeField] private TextMeshProUGUI levelNumber;
        [SerializeField] private TextMeshProUGUI experienceText;
        [SerializeField] private Slider experienceSlider;
        [SerializeField] private LevelUpButton levelUpButton;

        private ILevelPresenter presenter;

        private CompositeDisposable compositeDisposable = new();
        public void Initiate(IBigPresenter presenter)
        {
            compositeDisposable.Clear();

            foreach (ISmallPresenter smallPresenter in presenter.SmallPresenters)
            {
                if (smallPresenter is ILevelPresenter levelPresenter)
                {
                    this.presenter = levelPresenter;
                }
            }

            UpdateLevelText();
            UpdateXPText();
            UpdateSliderValues();

            this.presenter.CurrentExperience.Subscribe(HandleXPUpdateEvent).AddTo(compositeDisposable);
            this.presenter.Level.Subscribe(HandleLevelUpdateEvent).AddTo(compositeDisposable);
            this.levelUpButton.Button.onClick.AddListener(this.presenter.LevelUp);
        }

        public void UpdateLevelText()
        {
            int level = this.presenter.Level.Value;
            this.levelNumber.text = this.presenter.GetLevelText();
        }

        public void UpdateXPText()
        {
            int currentXP = this.presenter.CurrentExperience.Value;
            int requiredXP = this.presenter.RequiredExperience.Value;
            this.experienceText.text = this.presenter.GetXPText();
        }

        public void UpdateSliderValues()
        {
            this.experienceSlider.maxValue = this.presenter.RequiredExperience.Value;
            this.experienceSlider.value = this.presenter.CurrentExperience.Value;
        }

        private void HandleXPUpdateEvent(int _)
        {
            this.UpdateXPText();
            this.UpdateSliderValues();
            SetLevelUpButtonState();
        }

        private void SetLevelUpButtonState()
        {
            ButtonState state = this.presenter.CanLevelUp ? ButtonState.Active : ButtonState.Inactive;
            this.levelUpButton.SetState(state);
        }

        private void HandleLevelUpdateEvent(int _)
        {
            UpdateLevelText();
            SetLevelUpButtonState();
            UpdateXPText();
            UpdateSliderValues();
        }

        private void OnDestroy()
        {
            this.levelUpButton.Button.onClick.RemoveListener(this.presenter.LevelUp);
            this.compositeDisposable.Clear();
        }
    }

}
