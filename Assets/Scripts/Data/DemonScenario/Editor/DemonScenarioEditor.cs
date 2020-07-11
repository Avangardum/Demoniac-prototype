using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(DemonScenario))]
public class DemonScenarioEditor : Editor
{
    private DemonScenario _target;
    private float newTiming;
    private DemonButton newButton;
    private ButtonAction newButtonAction;

    private void OnEnable()
    {
        _target = (DemonScenario)target;
    }

    public override void OnInspectorGUI()
    {
        List<DemonScenarioElement> forDeletion = new List<DemonScenarioElement>();
        foreach (var element in _target)
        {
            GUILayout.BeginHorizontal();
                GUILayout.Label(element.Timing + " - " + (element.ButtonAction == ButtonAction.Press ? "Press " : "Release ") + element.Button.ToString());
                if (GUILayout.Button("Delete", GUILayout.Width(50)))
                    forDeletion.Add(element);
            GUILayout.EndHorizontal();
        }
        foreach (var element in forDeletion)
        {
            _target.Delete(element);
            EditorUtility.SetDirty(_target);
        }

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
            newTiming = EditorGUILayout.FloatField(newTiming);
            newButtonAction = (ButtonAction)EditorGUILayout.EnumPopup(newButtonAction);
            newButton = (DemonButton)EditorGUILayout.EnumPopup(newButton);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Add"))
        {
            _target.Add(new DemonScenarioElement { Timing = newTiming, Button = newButton, ButtonAction = newButtonAction });
            EditorUtility.SetDirty(_target);
        }
    }
}
