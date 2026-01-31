using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class ScreenControlScript : MonoBehaviour
{
    public bool isJumpedPressed = false;
    /// <summary>
    /// The position (X and Y distance) finger moved in previous frame
    /// </summary>
    public Vector2 fingerDeltaPosition;

    public Image JumpButton;
    public int JumpButtonFingerID = -1;
    private bool isMouseHoldingJump = false;
    private Vector2 lastMousePosition;

    public Image Joystick;
    private bool isMouseHoldingJoystick = false;
    private Vector2 joystickCenter;
    /// <summary>
    /// The direction from joystick center to current mouse/touch position (normalized)
    /// </summary>
    public Vector2 joystickDirection;
    
    private bool IsInRect(RectTransform rect, Vector2 screenPoint)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rect, screenPoint);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            
            // Check if clicking on joystick
            if (IsInRect(Joystick.rectTransform, mousePos))
            {
                Debug.Log("Joystick pressed (mouse)");
                isMouseHoldingJoystick = true;
                // Get the center of the joystick in screen space
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    Joystick.rectTransform, 
                    Joystick.rectTransform.position, 
                    null, 
                    out joystickCenter);
                joystickCenter = Joystick.rectTransform.position;
                UpdateJoystickDirection(mousePos);
            }
            // Check if clicking on jump button
            else if (IsInRect(JumpButton.rectTransform, mousePos))
            {
                Debug.Log("Jump button pressed (mouse)");
                isJumpedPressed = true;
                isMouseHoldingJump = true;
                lastMousePosition = mousePos;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isMouseHoldingJoystick)
            {
                Debug.Log("Joystick released (mouse)");
                isMouseHoldingJoystick = false;
                joystickDirection = Vector2.zero;
            }
            if (isMouseHoldingJump)
            {
                Debug.Log("Jump button released (mouse)");
                isJumpedPressed = false;
                isMouseHoldingJump = false;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePos = Input.mousePosition;
            
            // Update joystick direction while holding
            if (isMouseHoldingJoystick)
            {
                UpdateJoystickDirection(currentMousePos);
            }
            // Update jump button delta while holding
            else if (isMouseHoldingJump)
            {
                // Calculate mouse delta position while holding
                fingerDeltaPosition = currentMousePos - lastMousePosition;
                lastMousePosition = currentMousePos;
            }
        }
    }
    
    private void UpdateJoystickDirection(Vector2 mousePosition)
    {
        // Calculate direction from joystick center to mouse position
        Vector2 direction = mousePosition - joystickCenter;
        
        // Get the joystick's radius (half of its width or height, whichever is smaller)
        float joystickRadius = Mathf.Min(Joystick.rectTransform.rect.width, Joystick.rectTransform.rect.height) * 0.5f;
        
        // Clamp the direction to the joystick radius
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
    }
}