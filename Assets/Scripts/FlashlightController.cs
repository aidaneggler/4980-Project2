
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

    [Header("Unlocked Lights")]
    public bool hasWhiteLight = true;
    public bool hasUVLight = false;
    public bool hasRedLight = false;

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
        if (Keyboard.current == null || Keypad.IsUsingKeypad)
            return;

        // Toggle flashlight
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            flashlightOn = !flashlightOn;
            UpdateLight();
        }

        // Switch to white light
        if (Keyboard.current.digit1Key.wasPressedThisFrame &&
            hasWhiteLight)
        {
            currentLight = LightType.White;
            UpdateLight();
        }

        // Switch to UV light
        if (Keyboard.current.digit2Key.wasPressedThisFrame &&
            hasUVLight)
        {
            currentLight = LightType.UV;
            UpdateLight();
        }

        // Switch to red light
        if (Keyboard.current.digit3Key.wasPressedThisFrame &&
            hasRedLight)
        {
            currentLight = LightType.Red;
            UpdateLight();
        }
    }

    public void UnlockLight(LightType lightType)
    {
        switch (lightType)
        {
            case LightType.White:
                hasWhiteLight = true;
                break;

            case LightType.UV:
                hasUVLight = true;
                break;

            case LightType.Red:
                hasRedLight = true;
                break;
        }

        Debug.Log("Unlocked flashlight: " + lightType);
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
