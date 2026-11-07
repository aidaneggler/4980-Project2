
using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public FlashlightController.LightType lightType;

    public void Collect(FlashlightController controller)
    {
        if (controller == null)
            return;

        controller.UnlockLight(lightType);
        GameHUD.Instance?.ShowMessage(lightType + " Flashlight picked up!");
        Debug.Log("Collected " + lightType + " flashlight!");
        GameHUD.Instance?.ShowMessage(lightType + " Flashlight picked up!");
        Destroy(gameObject);
    }
}
