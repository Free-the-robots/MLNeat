using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    [System.Serializable]
    public class Patterns
    {
        public List<int> enemyNB = new List<int>();
        public List<int> enemyLife = new List<int>();
        public List<string> enemyPattern = new List<string>();
        public List<string> enemyWeapon = new List<string>();
        public List<bool> enemyFlipped = new List<bool>();
        public List<Vector3> enemyOffset = new List<Vector3>();
    }

    [System.Serializable]
    public class Levels
    {
        public List<float> periods = new List<float>();
        public List<Patterns> patterns = new List<Patterns>();
    }
}
