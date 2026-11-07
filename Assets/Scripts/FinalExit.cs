using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class FinalExit : MonoBehaviour
{
    private bool hasWon = false;
    private int winFrame;

    private void OnTriggerEnter(Collider other)
    {
        if (hasWon)
            return;

        if (other.GetComponent<CharacterController>() == null)
            return;

        hasWon = true;
        winFrame = Time.frameCount;

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (!hasWon || Time.frameCount == winFrame)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            ReturnToMenu();
        }
    }

    private void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void OnGUI()
    {
        if (!hasWon)
            return;

        GUIStyle titleStyle = new GUIStyle(GUI.skin.label);
        titleStyle.fontSize = 48;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.normal.textColor = Color.white;

        GUIStyle instructionStyle = new GUIStyle(titleStyle);
        instructionStyle.fontSize = 24;

        GUI.Label(
            new Rect(0, Screen.height / 2 - 80, Screen.width, 70),
            "YOU ESCAPED!",
            titleStyle
        );

        GUI.Label(
            new Rect(0, Screen.height / 2, Screen.width, 50),
            "Press E to Return to Menu",
            instructionStyle
        );
    }
}