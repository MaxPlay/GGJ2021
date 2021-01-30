using UnityEngine;
using UnityEngine.UI;
using GGJ.Levels;

namespace GGJ.UI
{
    public class UIMainMenu : IUIView
    {

        public static UIMainMenu Instance;
        [SerializeField] GameObject panelLogin;
        [SerializeField] private LevelRegistry levelRegistry;

        public override void SetActive(bool setActive)
        {
            panelLogin.SetActive(setActive);
        }

        void Awake()
        {
            if (Instance != null) { Destroy(this); } else { Instance = this; }
        }

        public override void UIUpdate(float deltaTime)
        {
            if (UIController.StackPosition(this) == 0) { HandleInputs(); }
        }

        void HandleInputs()
        {
        }

        public void SetSoundVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);

            // TODO: Now do something with the volume!
        }

        public void ButtonQuit()
        {
#if !UNITY_EDITOR
            Application.Quit();
#else
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
        }

        public void ButtonShowLevelSelect()
        {
            Debug.LogError("Should show the level select, but this method is not yet implemented");
        }
    }
}
