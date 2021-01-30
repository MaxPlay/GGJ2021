using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;

    [SerializeField]
    GameObject pauseUiWindow;

    const string mainMenuSceneName = "MainMenu";

    private void Awake()
    {
        isPaused = false;
        Time.timeScale = isPaused ? 0 : 1;
        pauseUiWindow.SetActive(isPaused);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPause();
        }
    }

    public void SwitchPause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseUiWindow.SetActive(isPaused);
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
