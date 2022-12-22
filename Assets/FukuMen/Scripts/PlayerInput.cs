using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IInput
{
    public bool IsJump(GameObject go)
    {
        return Input.GetButtonDown("Jump");
    }

    public bool IsSkill()
    {
        return Input.GetKeyDown(KeyCode.Return);
    }
}
