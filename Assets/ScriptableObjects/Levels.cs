using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{

    [Serializable]
    public struct Level
    {
        public int susCount;
        public int n;

    }
    [CreateAssetMenu(menuName = "Levels", fileName = "Levels")]
    public class Levels : UnityEngine.ScriptableObject
    {
        public List<Level> levels;
    }
}