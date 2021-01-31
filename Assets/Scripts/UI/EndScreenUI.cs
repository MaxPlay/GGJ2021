using GGJ.Levels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text titleText;

    [SerializeField]
    GameObject uiObject;

    [SerializeField]
    LevelRegistry levels;

    const string victoryText = "VICTORY";
    const string FailureText = "FAILED";

    const string mainMenuSceneName = "MainMenu";

    private void Awake()
    {
        uiObject.SetActive(false);
    }

    public void OpenEndScreen(bool isVictory)
    {
        titleText.text = isVictory ? victoryText : FailureText;
        uiObject.SetActive(true);
        PlayerCharacter.inputBlocked = true;
    }

    public void RestartLevel()
    {
        levels.RestartLevel();
    }

    public void NextLevel()
    {
        levels.StartNextlevel();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
