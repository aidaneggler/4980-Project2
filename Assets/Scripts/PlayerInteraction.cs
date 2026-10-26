
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionDistance = 3f;
    public Camera playerCamera;
    public FlashlightController flashlightController;

    void Update()
    {
        if (Keyboard.current == null)
            return;
        if (Keypad.IsUsingKeypad)
            return;
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        if (playerCamera == null)
            return;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {

            FlashlightPickup pickup =
                hit.collider.GetComponentInParent<FlashlightPickup>();

            if (pickup != null)
            {
                pickup.Collect(flashlightController);
                return;
            }
            Keypad keypad =
                hit.collider.GetComponentInParent<Keypad>();

            if (keypad != null)
            {
                keypad.OpenKeypad();
            }

            LightSwitch lightSwitch =
        hit.collider.GetComponentInParent<LightSwitch>();

        if (lightSwitch != null)
        {
            LightReactiveObject reactiveObject =
                lightSwitch.GetComponent<LightReactiveObject>();

            if (reactiveObject != null &&
                !reactiveObject.IsRevealed)
            {
                return;
            }
            lightSwitch.Activate();
        }
        }
    }
}
