using Lessons.Architecture.PM;
using TMPro;
using UnityEngine;

public class StatView : MonoBehaviour, IViewable
{

    [SerializeField] private StatSlot[] statSlots;

    private PlayerStat[] existingStats;

    private IStatsPresenter presenter;

    public void Initiate(IPresenter presenter)
    {
        foreach (ISmallPresenter smallPresenter in presenter.SmallPresenters)
        {
            if (smallPresenter is IStatsPresenter statPresenter)
            {
                this.presenter = statPresenter;
            }
        }

        SetupStats();
        this.presenter.StatsUpdateEvent += UpdateStats;
    }
    private void SetupStats()
    {
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
    }

    public void UpdateStatsText(StatSlot statTo, PlayerStat statFrom)
    {
        TextMeshProUGUI textMeshPro = statTo.StatObject.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = $"{statFrom.Name} : {statFrom.Value}";
    }


    private void UpdateStats()
    {
        for (int i = 0; i < this.existingStats.Length; i++)
        {
            UpdateStatsText(this.statSlots[i], this.existingStats[i]);
        }
    }

    private void OnDestroy()
    {
        this.presenter.StatsUpdateEvent -= UpdateStats;
    }

}
