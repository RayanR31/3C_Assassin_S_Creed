using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class InputManager : MonoBehaviour
{
    private Vector2 InputMove;
    private bool InputJump;
    protected void SetInputMove(InputAction.CallbackContext context)
    {
        InputMove = context.ReadValue<Vector2>();
    }

    public Vector2 GetInputMove()
    {
        return InputMove;
    }
    public void SetInputJump(InputAction.CallbackContext context)
    {
        if (context.performed && GameManager.instance.dataController.currentState == DataController.State.move)
        {
            InputJump = true;
        }
    }
    public void CancelInputJump()
    {
        InputJump = false;
    }
    public bool GetInputJump()
    {
        return InputJump;
    }
}
