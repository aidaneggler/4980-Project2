
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private bool hasWon = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasWon)
            return;

        CharacterController player =
            other.GetComponent<CharacterController>();

        if (player != null)
        {
            hasWon = true;

            Debug.Log("YOU ESCAPED!");

            Time.timeScale = 0f;
        }
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
                Screen.height / 2 - 50,
                500,
                100
            ),
            "YOU ESCAPED!",
            style
        );
    }
}
