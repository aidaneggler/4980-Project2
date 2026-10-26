
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

        Debug.Log("Collected " + lightType + " flashlight!");

        Destroy(gameObject);
    }
}
