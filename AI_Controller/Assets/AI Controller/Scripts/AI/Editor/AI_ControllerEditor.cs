using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AI_Controller))]
public class AI_ControllerEditor : Editor
{
    private AI_Controller trg;
    private AI_PresetsSave presetsData;
    string[] _aiPresets;
    string[] _aiState = new[] { "INSPECTING", "PATROLLING"};

    public override void OnInspectorGUI()
    {
        GUILayout.Label("AI PATROL POINTS LIST", EditorStyles.boldLabel);

        DrawDefaultInspector();

        if (presetsData == null)
            presetsData = Resources.Load<AI_PresetsSave>("ScriptObjects/AI_PresetsSave");

        trg = (AI_Controller)target;

        ReloadPreset();

        GUILayout.Space(10);
        GUILayout.Label("SELECT GLOBAL AI PRESET" , EditorStyles.boldLabel);
        trg._presetIndex = EditorGUILayout.Popup("", trg._presetIndex, _aiPresets);
        GUILayout.Label("TO CREATE A NEW AI PRESET OPEN 'AI/AI CONTROLLER WINDOW' IN TOP TOOLBAR", EditorStyles.boldLabel);

        GUILayout.Space(10);
        GUILayout.Label("AGENT IS: "   + _aiState[trg._agentState], EditorStyles.boldLabel);

        if(trg._pointsList != null)
            GUILayout.Label("AGENT TARGET: " + trg._pointsList._points[trg.pointIndex].name, EditorStyles.boldLabel);
    }

    private void ReloadPreset()
    {
        _aiPresets = new string[presetsData.presetSize];

        for (int i = 0; i < _aiPresets.Length; i++)
        {
            if (presetsData.presetName[i] != "")
                _aiPresets[i] = "Preset_" + (i + 1) + "_" + presetsData.presetName[i];
            else
                _aiPresets[i] = "Preset_" + (i + 1);
        }

        if (trg._presetIndex > _aiPresets.Length - 1)
            trg._presetIndex = _aiPresets.Length - 1;
    }
}
