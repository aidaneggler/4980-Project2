using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour
{
    public enum LightType
    {
        White,
        UV,
        Red
    }

    [Header("Flashlight Settings")]
    public Light flashlight;
    public bool flashlightOn = true;

    [Header("Current Light")]
    public LightType currentLight = LightType.White;

    void Start()
    {
        if (flashlight == null)
        {
            flashlight = GetComponent<Light>();
        }

        UpdateLight();
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        // Toggle flashlight
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            flashlightOn = !flashlightOn;
            UpdateLight();
        }

        // Switch wavelengths
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentLight = LightType.White;
            UpdateLight();
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentLight = LightType.UV;
            UpdateLight();
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentLight = LightType.Red;
            UpdateLight();
        }
    }

    void UpdateLight()
    {
        flashlight.enabled = flashlightOn;

        switch (currentLight)
        {
            case LightType.White:
                flashlight.color = Color.white;
                break;

            case LightType.UV:
                flashlight.color = new Color(0.5f, 0f, 1f);
                break;

            case LightType.Red:
                flashlight.color = Color.red;
                break;
        }
    }
}