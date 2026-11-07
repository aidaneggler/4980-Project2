using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControlsMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject controlsPanel;

    [Header("Player References")]
    public FirstPersonController playerController;
    public PlayerInteraction playerInteraction;
    public FlashlightController flashlightController;

    private bool controlsOpen = false;

    void Start()
    {
        controlsOpen = false;
        controlsPanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            ToggleControls();
        }
    }

    public void ToggleControls()
    {
        controlsOpen = !controlsOpen;

        controlsPanel.SetActive(controlsOpen);

        SetPlayerControls(!controlsOpen);

        if (controlsOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
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
}