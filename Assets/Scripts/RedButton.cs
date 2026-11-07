
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
        if (activated)
            return;

        activated = true;
        GameHUD.Instance?.ShowMessage("Button activated!");
        Debug.Log(gameObject.name + " activated!");

        if (reactiveObject != null)
        {
            reactiveObject.enabled = false;
        }

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

        if (puzzleManager != null)
        {
            puzzleManager.ButtonActivated();
        }
    }
}
