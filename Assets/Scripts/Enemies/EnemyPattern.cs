using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "EPath", menuName = "Enemy/EnemyPattern", order = 1)]
public class EnemyPattern : ScriptableObject
{
    public Vector3 Start;
    public List<Vector3> path;
    public List<float> time;
    public List<float> waitTime;

    public float particleFreq = 10f;
    public int periodOn = 10;
    public int periodOff = 10;
}
