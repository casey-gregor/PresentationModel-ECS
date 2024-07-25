using Lessons.Architecture.PM;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatView : MonoBehaviour
{
    public void UpdateStatsText(IStatPresenter statFrom)
    {
        TextMeshProUGUI textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = $"{statFrom.Name} : {statFrom.Value}";
    }
}
