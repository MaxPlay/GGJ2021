using GGJ.Gameplay;
using GGJ.Levels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text titleText, scoreText, chestText;

    [SerializeField]
    GameObject uiObject;

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    LevelRegistry levels;

    const string victoryText = "VICTORY";
    const string FailureText = "FAILED";

    const string mainMenuSceneName = "MainMenu";

    private void Awake()
    {
        uiObject.SetActive(false);
    }

    private void Start()
    {
        gameManager.OnGameOver.AddListener(OpenEndScreen);
    }

    private void OpenEndScreen(bool isVictory, int score, int chests, LevelData levelData)
    {
        PlayerCharacter.inputBlocked = true;
        PauseMenu.pauseEnabled = false;
        titleText.text = isVictory ? victoryText : FailureText;
        scoreText.text = score.ToString() + "\n/" + levelData.RequiredShipCount;
        chestText.text = chests.ToString() + "\n/" + levelData.RequiredChestCount;
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
