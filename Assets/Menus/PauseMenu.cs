using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    public GameObject PauseMenuUI;

    PlayerControls controls;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        controls = new PlayerControls();
        controls.Dash.Pause.performed += ctx => ControllerPause();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Unpause()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        IsPaused = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void ControllerPause()
    {
        if (IsPaused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    void OnEnable()
    {
        controls.Dash.Enable();
    }

    void OnDisable()
    {
        controls.Dash.Disable();
    }
}
