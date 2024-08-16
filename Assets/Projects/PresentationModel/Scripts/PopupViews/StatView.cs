using Lessons.Architecture.PM;
using TMPro;
using UnityEngine;

public class StatView : MonoBehaviour
{
    public void UpdateStatsText(IStatPresenter statFrom)
    {
        TextMeshProUGUI textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = statFrom.GetStatText();
    }
}
