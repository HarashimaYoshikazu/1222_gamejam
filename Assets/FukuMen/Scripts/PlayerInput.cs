using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IJump
{
    public bool IsJump()
    {
        return Input.GetButtonDown("Jump");
    }
}
