using System.Collections.Generic;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public class AllStatsView : MonoBehaviour, IViewable
    {
        [SerializeField] private GameObject statViewPrefab;
        [SerializeField] private Transform statViewParent;

        private Dictionary<string, StatView> existingStats = new();

        private IAllStatsPresenter presenter;

        public void Initiate(IBigPresenter presenter)
        {
            if (existingStats.Count > 0)
            {
                foreach (StatView item in existingStats.Values)
                {
                    Destroy(item.gameObject);
                }
            }

            existingStats.Clear();

            foreach (ISmallPresenter smallPresenter in presenter.SmallPresenters)
            {
                if (smallPresenter is IAllStatsPresenter allStatPresenter)
                {
                    this.presenter = allStatPresenter;
                }

            }

            foreach (IStatPresenter statPresenter in this.presenter.StatPresenters)
            {
                StatView statView = Instantiate(statViewPrefab, statViewParent).GetComponent<StatView>();
                statView.UpdateStatsText(statPresenter);
                this.existingStats.Add(statPresenter.Name, statView);
            }

            this.presenter.StatsUpdateEvent += UpdateStats;
        }


        private void UpdateStats()
        {
            foreach (IStatPresenter statPresenter in this.presenter.StatPresenters)
            {
                this.existingStats[statPresenter.Name].UpdateStatsText(statPresenter);
            }
        }

        private void OnDestroy()
        {
            this.presenter.StatsUpdateEvent -= UpdateStats;
        }

    }
}

