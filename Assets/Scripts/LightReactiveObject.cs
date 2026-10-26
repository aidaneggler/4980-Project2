
using UnityEngine;

public class LightReactiveObject : MonoBehaviour
{
    [Header("Light Requirements")]
    public FlashlightController.LightType requiredLight =
        FlashlightController.LightType.UV;

    [Header("References")]
    public FlashlightController flashlightController;

    private Light flashlight;
    private Renderer[] objectRenderers;
    private Collider objectCollider;

    public bool IsRevealed { get; private set; }

    void Start()
    {
        if (flashlightController == null)
        {
            flashlightController =
                FindFirstObjectByType<FlashlightController>();
        }

        if (flashlightController == null)
        {
            Debug.LogError("No FlashlightController found!");
            enabled = false;
            return;
        }

        flashlight = flashlightController.flashlight;

        if (flashlight == null)
        {
            flashlight = flashlightController.GetComponent<Light>();
        }

        if (flashlight == null)
        {
            Debug.LogError("No flashlight Light found!");
            enabled = false;
            return;
        }

        objectRenderers = GetComponentsInChildren<Renderer>();
        objectCollider = GetComponent<Collider>();

        if (objectCollider == null)
        {
            Debug.LogError("LightReactiveObject needs a Collider!");
            enabled = false;
            return;
        }

        SetVisible(false);
    }

    void Update()
    {
        bool shouldReveal = CheckLight();
        SetVisible(shouldReveal);
    }

    bool CheckLight()
    {
        if (!flashlightController.flashlightOn ||
            !flashlight.enabled)
        {
            return false;
        }

        if (flashlightController.currentLight != requiredLight)
        {
            return false;
        }

        Vector3 targetPosition = objectCollider.bounds.center;

        Vector3 direction =
            targetPosition - flashlight.transform.position;

        float distance = direction.magnitude;

        if (distance > flashlight.range || distance < 0.01f)
        {
            return false;
        }

        float angle = Vector3.Angle(
            flashlight.transform.forward,
            direction
        );

        if (angle > flashlight.spotAngle / 2f)
        {
            return false;
        }

        RaycastHit hit;

        if (Physics.Raycast(
            flashlight.transform.position,
            direction.normalized,
            out hit,
            distance + 0.1f))
        {
            if (hit.collider != objectCollider)
            {
                return false;
            }

            return true;
        }

        return false;
    }

    void SetVisible(bool visible)
    {
        IsRevealed = visible;

        foreach (Renderer renderer in objectRenderers)
        {
            renderer.enabled = visible;
        }
    }
}