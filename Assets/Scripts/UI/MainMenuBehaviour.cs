using GGJ.Levels;
using UnityEngine;

namespace GGJ.UI
{
    public class MainMenuBehaviour : MonoBehaviour
    {
        [SerializeField]
        private LevelRegistry levelRegistry;

        public void StartGame()
        {
            levelRegistry.StartFirstLevel();
        }

        public void ShowLevelSelect()
        {
            Debug.LogError("Should show the level select, but this method is not yet implemented");
        }

        public void SetSoundVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);

            // TODO: Now do something with the volume!
        }

        public void Quit()
        {
#if !UNITY_EDITOR
            Application.Quit();
#else
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
        }
    }
}
