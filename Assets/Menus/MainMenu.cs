using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject PlayButton;
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

    public void OnHover()
    {
        PlayButton.GetComponent<Collider2D>().enabled = true;
        QuitButton.GetComponent<Collider2D>().enabled = true;
    }

    public void OnHoverStop()
    {
        PlayButton.GetComponent<Collider2D>().enabled = false;
        QuitButton.GetComponent<Collider2D>().enabled = false;
    }
}
