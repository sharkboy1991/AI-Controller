using UnityEditor;
using UnityEngine;

public class AI_Controller_Window : EditorWindow
{
    //presets
    string presetName;
    private AI_PresetsSave presetsData;
    string[] _aiPresets;
    int _aiPresetIndex;

    //patrol
    string[] _aiPatrol = new[] { "Cirlcular", "Reverse", "Random" };
    int _aiPatrolIndex;



    [MenuItem("AI/AI Controller Window")]
    public static void ShowWindow()
    {
        //close window in playmode
        if (Application.isPlaying)
            return;

        // Get existing open window or if none, make a new one:
        AI_Controller_Window window = (AI_Controller_Window)EditorWindow.GetWindow(typeof(AI_Controller_Window));
        window.Show();
    }

    private void OnEnable()
    {
        presetsData = Resources.Load<AI_PresetsSave>("ScriptObjects/AI_PresetsSave");

        ReloadPreset();
    }

    void OnGUI()
    {
        //close window in playmode
        if (Application.isPlaying)
            this.Close();

        //AI PRESET SETTINS
        PresetSettings();

        //AGENT PATROL
        PatrolOptions();
    }

    private void PresetSettings()
    {
        GUILayout.Label("PRESET SELECTION", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();

        _aiPresetIndex = EditorGUILayout.Popup("", _aiPresetIndex, _aiPresets);

        presetName = EditorGUILayout.TextField("", presetName);
        if (GUILayout.Button("CHANGE PRESET NAME"))
        {
            presetsData.presetName[_aiPresetIndex] = presetName;
            GUI.FocusControl(null);
            presetName = "";
            ReloadPreset();
        }

        EditorGUILayout.EndHorizontal();



        EditorGUILayout.Space(10);
        GUILayout.Label("PRESET OPTIONS", EditorStyles.boldLabel);


        EditorGUILayout.BeginHorizontal();
        //add new preset selection
        if (GUILayout.Button("ADD NEW PRESET"))
        {
            if (presetsData.presetSize < 100)
            {
                presetsData.presetSize++;
                _aiPresetIndex = presetsData.presetSize;
                ReloadPreset();
            }
        }

        //delete last preset selection
        if (GUILayout.Button("DELETE LAST PRESET"))
        {
            if (presetsData.presetSize > 1)
            {
                presetsData.presetName[presetsData.presetSize - 1] = "";
                presetsData.presetSize--;
                ReloadPreset();
            }
        }

        //delete all preset selection
        if (GUILayout.Button("DELETE ALL PRESETS"))
        {
            if (presetsData.presetSize >= 1)
            {
                for (int i = 0; i < presetsData.presetName.Length; i++)
                    presetsData.presetName[i] = "";

                presetsData.presetSize = 1;
                ReloadPreset();
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    private void ReloadPreset() 
    {
        _aiPresets = new string[presetsData.presetSize];

        for (int i = 0; i < _aiPresets.Length; i++)
        {
            if(presetsData.presetName[i] != "")
                _aiPresets[i] = "Preset_" + (i + 1) + "_" + presetsData.presetName[i];
            else
                _aiPresets[i] = "Preset_" + (i + 1);
        }


        if (_aiPresetIndex > _aiPresets.Length - 1)
            _aiPresetIndex = _aiPresets.Length - 1;
    }

    private void PatrolOptions() 
    {
        EditorGUILayout.Space(25);

        string t = "AGENT PATROL";
        if (presetsData.agentPatrol[_aiPresetIndex])
            t += " (ENABLED)";
        else
            t += " (DISABLED)";

        presetsData.agentPatrol[_aiPresetIndex] = EditorGUILayout.BeginToggleGroup(t, presetsData.agentPatrol[_aiPresetIndex]);//>>>>

        presetsData.agentPatrolType[_aiPresetIndex] = EditorGUILayout.Popup("Patrol Options", presetsData.agentPatrolType[_aiPresetIndex], _aiPatrol);


        //inspect delay
        EditorGUILayout.Space(10);
        GUILayout.Label("PATROL INSPECT DELAY (MIN:" + presetsData.agentMinDelay[_aiPresetIndex] + " / MAX:" + presetsData.agentMaxDelay[_aiPresetIndex] + ")", EditorStyles.boldLabel);
        EditorGUILayout.MinMaxSlider(ref presetsData.agentMinDelay[_aiPresetIndex], ref presetsData.agentMaxDelay[_aiPresetIndex], 0f, 10f);

        EditorGUILayout.EndToggleGroup();//<<<<<
    }

    private void OnDisable()
    {

    }
}