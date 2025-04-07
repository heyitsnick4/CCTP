using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject PlayButton;
    [SerializeField] private GameObject MainMenuButton;
    [SerializeField] private GameObject QuitButton;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenuL()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnHoverPlay()
    {
        PlayButton.SetActive(true);
    }

    public void OnHoverStopPlay()
    {
        PlayButton.SetActive(false);
    }

    public void OnHoverQuit()
    {
        QuitButton.SetActive(true);
    }

    public void OnHoverStopQuit()
    {
        QuitButton.SetActive(false);
    }

    public void OnHoverMainMenu()
    {
        MainMenuButton.SetActive(true);
    }

    public void OnHoverStopMainMenuQuit()
    {
        MainMenuButton.SetActive(false);
    }
}
