
using UnityEngine;
using UnityEngine.InputSystem;

public class Keypad : MonoBehaviour
{
    [Header("Keypad Settings")]
    public string correctCode = "4271";
    public int maxDigits = 4;

    [Header("Door to Unlock")]
    public GameObject cabinetDoor;

    private string enteredCode = "";
    private bool isOpen = false;
    private bool isUnlocked = false;
    private string message = "";
    public static bool IsUsingKeypad { get; private set; }

    void Update()
    {
        if (!isOpen || Keyboard.current == null)
            return;

        Keyboard keyboard = Keyboard.current;
        if (keyboard.fKey.wasPressedThisFrame)
        {
            CloseKeypad();
            return;
        }

        if (keyboard.digit0Key.wasPressedThisFrame)
            AddDigit("0");

        if (keyboard.digit1Key.wasPressedThisFrame)
            AddDigit("1");

        if (keyboard.digit2Key.wasPressedThisFrame)
            AddDigit("2");

        if (keyboard.digit3Key.wasPressedThisFrame)
            AddDigit("3");

        if (keyboard.digit4Key.wasPressedThisFrame)
            AddDigit("4");

        if (keyboard.digit5Key.wasPressedThisFrame)
            AddDigit("5");

        if (keyboard.digit6Key.wasPressedThisFrame)
            AddDigit("6");

        if (keyboard.digit7Key.wasPressedThisFrame)
            AddDigit("7");

        if (keyboard.digit8Key.wasPressedThisFrame)
            AddDigit("8");

        if (keyboard.digit9Key.wasPressedThisFrame)
            AddDigit("9");

        if (keyboard.backspaceKey.wasPressedThisFrame)
        {
            if (enteredCode.Length > 0)
            {
                enteredCode = enteredCode.Substring(
                    0,
                    enteredCode.Length - 1
                );
            }
            message = "";
        }

        if (keyboard.enterKey.wasPressedThisFrame ||
            keyboard.numpadEnterKey.wasPressedThisFrame)
        {
            CheckCode();
        }
    }

    public void OpenKeypad()
    {
        if (isUnlocked)
        {
            Debug.Log("Keypad already unlocked!");
            return;
        }
        isOpen = true;
        IsUsingKeypad = true;
        enteredCode = "";
        message = "";
    }

    void CloseKeypad()
    {
        isOpen = false;
        IsUsingKeypad = false;
    }

    void AddDigit(string digit)
    {
        if (enteredCode.Length >= maxDigits)
            return;

        enteredCode += digit;
        message = "";
    }

    void CheckCode()
    {
        if (enteredCode == correctCode)
        {
            isUnlocked = true;
            Debug.Log("Correct code! Cabinet unlocked.");

            if (cabinetDoor != null)
            {
                cabinetDoor.SetActive(false);
            }
            CloseKeypad();
        }
        else
        {
            message = "Incorrect Code";
            enteredCode = "";
        }
    }

    void OnGUI()
    {
        if (!isOpen)
            return;

        float width = 320f;
        float height = 220f;

        float x = (Screen.width - width) / 2f;
        float y = (Screen.height - height) / 2f;

        GUI.Box(
            new Rect(x, y, width, height),
            "SECURITY KEYPAD"
        );

        GUI.Label(
            new Rect(x + 90, y + 50, 200, 30),
            "Code: " + enteredCode.PadRight(maxDigits, '_')
        );

        GUI.Label(
            new Rect(x + 90, y + 90, 200, 30),
            message
        );

        GUI.Label(
            new Rect(x + 45, y + 140, 280, 30),
            "Type code, Enter to submit"
        );

        GUI.Label(
            new Rect(x + 90, y + 170, 200, 30),
            "Press F to close"
        );
    }

    void OnDisable()
    {
        if (isOpen)
        {
            CloseKeypad();
        }
    }
}
