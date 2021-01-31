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
        [SerializeField] Animator uiAnimator;

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

        public void BackToMainMenuButton()
        {
            uiAnimator.SetBool("IsLevelSelect", false);
        }

        public void ButtonShowLevelSelect()
        {
            uiAnimator.SetBool("IsLevelSelect", true);
        }

        public void StartLevel(int level)
        {
            levelRegistry.StartLevel(level);
        }
    }
}
