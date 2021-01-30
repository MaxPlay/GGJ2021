using UnityEngine;

namespace GGJ.Levels
{
    [CreateAssetMenu(menuName = "GGJ/Level Registry")]
    public class LevelRegistry : ScriptableObject
    {
        [SerializeField]
        private LevelData[] levels;

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
                Debug.LogWarning("Tried to request a level index that is out of range.");
                return;
            }

            levels[index].StartLevel();
        }
    }
}
