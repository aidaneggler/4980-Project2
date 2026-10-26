using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    private bool hasWon = false;
    private int winFrame = -1;

    void Update()
    {
        if (hasWon &&
            Time.frameCount > winFrame &&
            Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasWon)
            return;

        CharacterController player =
            other.GetComponent<CharacterController>();

        if (player != null)
        {
            hasWon = true;
            winFrame = Time.frameCount;
            Debug.Log("YOU ESCAPED!");
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    void OnGUI()
    {
        if (!hasWon)
            return;

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 40;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.green;

        GUI.Label(
            new Rect(
                Screen.width / 2 - 250,
                Screen.height / 2 - 75,
                500,
                100
            ),
            "YOU ESCAPED!",
            style
        );

        GUIStyle restartStyle = new GUIStyle(GUI.skin.label);
        restartStyle.fontSize = 24;
        restartStyle.alignment = TextAnchor.MiddleCenter;
        restartStyle.normal.textColor = Color.white;

        GUI.Label(
            new Rect(
                Screen.width / 2 - 250,
                Screen.height / 2 + 25,
                500,
                50
            ),
            "Press E to Restart",
            restartStyle
        );
    }
}