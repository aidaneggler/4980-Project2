using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Win : MonoBehaviour
{
    [Header("UI")]
    public GameObject victoryPanel;

    [Header("Player")]
    public FirstPersonController playerController;
    public PlayerInteraction playerInteraction;
    public FlashlightController flashlightController;

    private bool hasWon = false;

    private void Start()
    {
        victoryPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasWon)
            return;

        if (other.GetComponent<CharacterController>() == null)
            return;

        WinGame();
    }

    private void WinGame()
    {
        hasWon = true;

        victoryPanel.SetActive(true);

        if (playerController != null)
            playerController.enabled = false;

        if (playerInteraction != null)
            playerInteraction.enabled = false;

        if (flashlightController != null)
            flashlightController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}