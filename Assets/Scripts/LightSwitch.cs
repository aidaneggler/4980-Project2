
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [Header("Switch Settings")]
    public ExitDoor exitDoor;

    private bool activated = false;

    public void Activate()
    {
        if (activated)
            return;

        activated = true;
        GameHUD.Instance?.ShowMessage("Exit Door Opened!");
        Debug.Log("Hidden switch activated!");
        if (exitDoor != null)
        {
            exitDoor.UnlockDoor();
        }
    }
}
