using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ.Levels
{
    [CreateAssetMenu(menuName = "GGJ/Level Registry")]
    public class LevelRegistry : ScriptableObject
    {
        [SerializeField]
        private LevelData[] levels;
        private int currentLevel = -1;

        const string mainMenuSceneName = "MainMenu";

        public void RestartLevel()
        {
            levels[currentLevel].StartLevel();
        }

        public void StartFirstLevel()
        {
            StartLevel(0);
        }

        public void StartLevel(int index)
        {
            if (levels.Length == 0)
            {
                Debug.LogWarning("No levels in level data.");
                return;
            }

            if (levels.Length <= index)
            {
                Debug.LogWarning("Tried to request a level index that is out of range. Propably End of Game.");
                currentLevel = -1;
                SceneManager.LoadScene(mainMenuSceneName);
            }
            if(index < 0)
            {
                Debug.LogError("Tried to request a level index that is less that 0");
            }
            currentLevel = index;
            levels[index].StartLevel();
        }

        internal void StartNextlevel()
        {
            StartLevel(currentLevel + 1);
        }
    }
}
