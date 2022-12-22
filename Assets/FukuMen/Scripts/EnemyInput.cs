using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyInput : IJump 
{
    [SerializeField]
    private float _waitTime = 3f;

    //public bool IsJump()
    //{
    //    Debug.DrawLine(Vector3.zero, Vector3.forward);
    //    if (Physics.Raycast(Vector3.zero, Vector3.forward, 5f, LayerMask.NameToLayer("Hurdle")))
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    float _timer = 0;
    public bool IsJump()
    {
        _timer += Time.deltaTime;
        if(_timer >= _waitTime)
        {
            _timer = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

}
