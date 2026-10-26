
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
        Debug.Log("Hidden switch activated!");
        if (exitDoor != null)
        {
            exitDoor.UnlockDoor();
        }
    }
}
