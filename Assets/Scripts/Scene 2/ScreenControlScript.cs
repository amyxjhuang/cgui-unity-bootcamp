using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class ScreenControlScript : MonoBehaviour
{
    public bool isJumpedPressed = false;
    public Vector2 fingerDeltaPosition;

    public Image JumpButton;
    public int JumpButtonFingerID = -1;
    private bool isMouseHoldingJump = false;
    private Vector2 lastMousePosition;

    [Header("Joystick")]
    public Image Joystick;
    private bool isMouseHoldingJoystick = false;
    private Vector2 joystickCenter;
    public Vector2 joystickDirection;

    public Rigidbody player;
    public float joystickSensitivity = 2f;
    
    [Header("Camera Rotation")]
    public Camera mainCamera;
    public float rotationSensitivity = .2f;
    private bool isDraggingCamera = false;
    private Vector2 lastDragPosition;
    
    private bool IsInRect(RectTransform rect, Vector2 screenPoint)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rect, screenPoint);
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        
        if (Input.GetMouseButtonDown(0))
        {
            if (IsInRect(Joystick.rectTransform, mousePos))
            {
                isMouseHoldingJoystick = true;
                Vector3[] corners = new Vector3[4];
                Joystick.rectTransform.GetWorldCorners(corners);
                joystickCenter = new Vector2((corners[0].x + corners[2].x) * 0.5f, (corners[0].y + corners[2].y) * 0.5f);
                UpdateJoystickDirection(mousePos);
            }
            else if (IsInRect(JumpButton.rectTransform, mousePos))
            {
                isJumpedPressed = isMouseHoldingJump = true;
                lastMousePosition = mousePos;
            }
            else
            {
                isDraggingCamera = true;
                lastDragPosition = mousePos;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isMouseHoldingJoystick) { isMouseHoldingJoystick = false; joystickDirection = Vector2.zero; }
            if (isMouseHoldingJump) { isJumpedPressed = isMouseHoldingJump = false; }
            isDraggingCamera = false;
        }
        else if (Input.GetMouseButton(0))
        {
            if (isMouseHoldingJoystick)
                UpdateJoystickDirection(mousePos);
            else if (isMouseHoldingJump)
            {
                fingerDeltaPosition = mousePos - lastMousePosition;
                lastMousePosition = mousePos;
                transform.rotation = Quaternion.LookRotation(joystickDirection, Vector3.up);
                player.linearVelocity = new Vector3(joystickDirection.x * joystickSensitivity, 5, joystickDirection.y * joystickSensitivity);
            }
            else if (isDraggingCamera && mainCamera != null)
            {
                RotateCamera(mousePos - lastDragPosition);
                lastDragPosition = mousePos;
            }
        }
    }
    
    private void UpdateJoystickDirection(Vector2 mousePosition)
    {
        Vector2 direction = mousePosition - joystickCenter;
        
        float joystickRadius = Mathf.Min(Joystick.rectTransform.rect.width, Joystick.rectTransform.rect.height) * 0.5f;
        
        if (direction.magnitude > joystickRadius)
        {
            direction = direction.normalized * joystickRadius;
        }
        
        // Normalize the direction (values will be between -1 and 1)
        if (joystickRadius > 0)
        {
            joystickDirection = direction / joystickRadius;
        }
        else
        {
            joystickDirection = Vector2.zero;
        }
        
        Debug.Log($"Joystick: {joystickDirection}");
        player.linearVelocity = new Vector3(joystickDirection.x * joystickSensitivity, 0f, joystickDirection.y * joystickSensitivity);
    }
    
    private void RotateCamera(Vector2 dragDelta)
    {
        // Rotate horizontally (Y axis) based on horizontal drag
        mainCamera.transform.Rotate(0, -dragDelta.x * rotationSensitivity, 0, Space.World);
        
        // Rotate vertically (X axis) based on vertical drag
        mainCamera.transform.Rotate(dragDelta.y * rotationSensitivity, 0, 0, Space.Self);
    }
}