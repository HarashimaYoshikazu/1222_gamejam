using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IJump
{
    public bool IsJump(GameObject go)
    {
        return Input.GetButtonDown("Jump");
    }
}
