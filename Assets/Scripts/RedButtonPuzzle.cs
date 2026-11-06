using UnityEngine;

public class RedButtonPuzzle : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public int requiredButtons = 3;
    public GameObject exitDoor;

    private int activatedButtons = 0;
    private bool puzzleCompleted = false;

    public void ButtonActivated()
    {
        if (puzzleCompleted)
            return;

        activatedButtons++;

        Debug.Log("Buttons activated: " +
            activatedButtons + "/" + requiredButtons);

        if (activatedButtons >= requiredButtons)
        {
            CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        puzzleCompleted = true;

        Debug.Log("All buttons activated! Exit unlocked!");

        if (exitDoor != null)
        {
            exitDoor.SetActive(false);
        }
    }
}