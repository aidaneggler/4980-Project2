using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject mainMenuPanel;
    public GameObject controlsPanel;

    void Start()
    {
        Time.timeScale = 1f;

        mainMenuPanel.SetActive(true);
        controlsPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Level_1");
    }

    public void ShowControls()
    {
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        controlsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}