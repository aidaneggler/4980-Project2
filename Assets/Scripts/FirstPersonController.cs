using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Camera")]
    public Transform playerCamera;
    public float mouseSensitivity = 0.1f;
    public float maxLookAngle = 90f;

    private CharacterController controller;
    private Vector3 velocity;
    private float cameraRotationX;
    private bool cursorLocked = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        LockCursor();
    }

    void Update()
    {
        HandleCursor();

        if (Keypad.IsUsingKeypad)
        {
            ApplyGravity();
            return;
        }

        if (cursorLocked)
        {
            HandleMouseLook();
            HandleMovement();
        }

        ApplyGravity();
    }

    void HandleMovement()
    {
        if (Keyboard.current == null)
            return;

        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            input.y += 1;

        if (Keyboard.current.sKey.isPressed)
            input.y -= 1;

        if (Keyboard.current.aKey.isPressed)
            input.x -= 1;

        if (Keyboard.current.dKey.isPressed)
            input.x += 1;

        input = Vector2.ClampMagnitude(input, 1f);

        Vector3 move =
            transform.right * input.x +
            transform.forward * input.y;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        if (Mouse.current == null || playerCamera == null)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSensitivity;
        float mouseY = mouseDelta.y * mouseSensitivity;

        cameraRotationX -= mouseY;

        cameraRotationX = Mathf.Clamp(
            cameraRotationX,
            -maxLookAngle,
            maxLookAngle
        );

        playerCamera.localRotation =
            Quaternion.Euler(cameraRotationX, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void HandleCursor()
    {
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            UnlockCursor();
        }

        if (!cursorLocked &&
            Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            LockCursor();
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorLocked = true;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorLocked = false;
    }

    void OnDisable()
    {
        UnlockCursor();
    }
}