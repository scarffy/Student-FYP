using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FYP.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "FYP/Data/LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        /// <summary>
        /// All Levels/Scenes to load
        /// </summary>
        public List<Object> levels;
    }
}