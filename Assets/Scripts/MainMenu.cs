using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject mainMenuPanel;
    public GameObject controlsPanel;

    [Header("Player References")]
    public FirstPersonController playerController;
    public PlayerInteraction playerInteraction;
    public FlashlightController flashlightController;

    private bool gameStarted = false;
    private bool controlsOpen = false;

    void Start()
    {
        SetPlayerControls(false);

        mainMenuPanel.SetActive(true);
        controlsPanel.SetActive(false);

        UnlockCursor();
    }

    void Update()
    {
        if (!gameStarted || Keyboard.current == null)
            return;

        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            ToggleControls();
        }
    }

    public void StartGame()
    {
        gameStarted = true;

        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(false);

        controlsOpen = false;

        SetPlayerControls(true);
        LockCursor();
    }

    public void ShowControls()
    {
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);

        controlsOpen = true;

        UnlockCursor();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    void ToggleControls()
    {
        if (controlsOpen)
        {
            controlsOpen = false;
            controlsPanel.SetActive(false);

            SetPlayerControls(true);
            LockCursor();
        }
        else
        {
            controlsOpen = true;
            controlsPanel.SetActive(true);

            SetPlayerControls(false);
            UnlockCursor();
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    void SetPlayerControls(bool enabled)
    {
        if (playerController != null)
            playerController.enabled = enabled;

        if (playerInteraction != null)
            playerInteraction.enabled = enabled;

        if (flashlightController != null)
            flashlightController.enabled = enabled;
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}