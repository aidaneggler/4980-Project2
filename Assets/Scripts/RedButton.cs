
using UnityEngine;

public class RedButton : MonoBehaviour
{
    [Header("Button Settings")]
    public RedButtonPuzzle puzzleManager;

    private bool activated = false;
    private Renderer[] buttonRenderers;
    private LightReactiveObject reactiveObject;

    void Start()
    {
        buttonRenderers = GetComponentsInChildren<Renderer>();
        reactiveObject = GetComponent<LightReactiveObject>();
    }

    public void Activate()
    {
        // Button can only be activated once
        if (activated)
            return;

        activated = true;

        Debug.Log(gameObject.name + " activated!");

        // Stop the button from hiding again
        if (reactiveObject != null)
        {
            reactiveObject.enabled = false;
        }

        // Turn the button white and keep it visible
        foreach (Renderer renderer in buttonRenderers)
        {
            renderer.enabled = true;

            foreach (Material material in renderer.materials)
            {
                if (material.HasProperty("_BaseColor"))
                {
                    material.SetColor("_BaseColor", Color.white);
                }
                else if (material.HasProperty("_Color"))
                {
                    material.color = Color.white;
                }
            }
        }

        // Tell puzzle manager
        if (puzzleManager != null)
        {
            puzzleManager.ButtonActivated();
        }
    }
}
