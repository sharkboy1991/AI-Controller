using UnityEngine;

[CreateAssetMenu(fileName = "AI_PresetsSave", menuName = "AI/SavePresets", order = 1)]
public class AI_PresetsSave : ScriptableObject
{
    //preset size counter
    public int presetSize = 1;

    //preset variables
    public string [] presetName = new string[100];  // preset name
    public bool[] agentPatrol = new bool[100];      // false=cant patrol, true=can patrol
    public int[] agentPatrolType = new int[100];    // 0=path, 1=random
    public float[] agentMinDelay = new float[100];     // min time delay when reach patrol point
    public float[] agentMaxDelay = new float[100];     // max time delay when reach patrol point 
}
