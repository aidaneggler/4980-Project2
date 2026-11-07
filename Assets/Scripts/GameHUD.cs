using UnityEngine;
using TMPro;
using System.Collections;

public class GameHUD : MonoBehaviour
{
    public static GameHUD Instance;

    [Header("UI")]
    public TMP_Text flashlightText;
    public TMP_Text notificationText;

    [Header("Player")]
    public FlashlightController flashlightController;

    private Coroutine messageCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        notificationText.text = "";
    }

    private void Update()
    {
        UpdateFlashlightDisplay();
    }

    private void UpdateFlashlightDisplay()
    {
        if (flashlightController == null || flashlightText == null)
            return;

        switch (flashlightController.currentLight)
        {
            case FlashlightController.LightType.White:
                flashlightText.text = "White Flashlight";
                break;

            case FlashlightController.LightType.UV:
                flashlightText.text = "UV Flashlight";
                break;

            case FlashlightController.LightType.Red:
                flashlightText.text = "Red Flashlight";
                break;
        }
    }

    public void ShowMessage(string message, float duration = 3f)
    {
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(
            DisplayMessage(message, duration)
        );
    }

    private IEnumerator DisplayMessage(string message, float duration)
    {
        notificationText.text = message;

        yield return new WaitForSeconds(duration);

        notificationText.text = "";

        messageCoroutine = null;
    }
}