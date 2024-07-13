using UnityEditor;
using UnityEngine;

namespace Lessons.Architecture.PM
{

    [CustomEditor(typeof(ActionHelper))]
    public class CustomEditorElements : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var actionHelper = (ActionHelper)target;
            if(GUILayout.Button("Show Popup"))
            {
                actionHelper.ShowPopup();
            }
            if (GUILayout.Button("AddXP"))
            {
                actionHelper.AddXP(actionHelper.XPToAdd);
            }
        }
    }

}
