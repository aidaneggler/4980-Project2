
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public bool isUnlocked = false;

    public void UnlockDoor()
    {
        if (isUnlocked)
            return;

        isUnlocked = true;
        Debug.Log("Exit door unlocked!");
        gameObject.SetActive(false);
    }
}
