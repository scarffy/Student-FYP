using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FYP.Data
{
    /// <summary>
    /// Type the name actual name again since object is not recognize  in build
    /// </summary>
    [CreateAssetMenu(fileName = "LevelData", menuName = "FYP/Data/LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        /// <summary>
        /// All Levels/Scenes to load
        /// </summary>
        public List<Object> levels;

        public List<string> levelsName;

    }
}