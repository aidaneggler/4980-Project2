using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour
{
    [Header("Flashlight")]
    public Light flashlight;
    public bool flashlightOn = true;

    void Start()
    {
        if (flashlight == null)
        {
            flashlight = GetComponent<Light>();
        }

        flashlight.enabled = flashlightOn;
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.fKey.wasPressedThisFrame)
        {
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        flashlightOn = !flashlightOn;
        flashlight.enabled = flashlightOn;
    }
}