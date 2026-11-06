using UnityEngine;
using TMPro;

public class LightReactiveObject : MonoBehaviour
{
    [Header("Light Requirements")]
    public FlashlightController.LightType requiredLight =
        FlashlightController.LightType.UV;

    [Header("References")]
    public FlashlightController flashlightController;

    [Header("Distance Fading")]
    public bool useDistanceFade = false;
    public float fullVisibilityDistance = 2f;
    public float fadeStartDistance = 8f;

    private Light flashlight;
    private Renderer[] objectRenderers;
    private Collider objectCollider;
    private TMP_Text textMesh;

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
        textMesh = GetComponentInChildren<TMP_Text>();

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

        // Adjust transparency based on distance
        if (useDistanceFade && textMesh != null && shouldReveal)
        {
            float distance = Vector3.Distance(
                flashlight.transform.position,
                objectCollider.bounds.center
            );

            float alpha = Mathf.InverseLerp(
                fadeStartDistance,
                fullVisibilityDistance,
                distance
            );

            textMesh.alpha = alpha;
        }
    }

    bool CheckLight()
    {
        // Flashlight must be on
        if (!flashlightController.flashlightOn ||
            !flashlight.enabled)
        {
            return false;
        }

        // Correct wavelength required
        if (flashlightController.currentLight != requiredLight)
        {
            return false;
        }

        Vector3 targetPosition = objectCollider.bounds.center;

        Vector3 direction =
            targetPosition - flashlight.transform.position;

        float distance = direction.magnitude;

        // Check range
        if (distance > flashlight.range || distance < 0.01f)
        {
            return false;
        }

        // Check spotlight cone
        float angle = Vector3.Angle(
            flashlight.transform.forward,
            direction
        );

        if (angle > flashlight.spotAngle / 2f)
        {
            return false;
        }

        // Check for obstacles
        RaycastHit hit;

        if (Physics.Raycast(
            flashlight.transform.position,
            direction.normalized,
            out hit,
            distance + 0.1f))
        {
            return hit.collider == objectCollider;
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

        if (!visible && textMesh != null)
        {
            textMesh.alpha = 0f;
        }
        else if (visible && textMesh != null && !useDistanceFade)
        {
            textMesh.alpha = 1f;
        }
    }
}