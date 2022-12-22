using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyInput : IInput 
{
    private const float _rayDistance = 10f;
    private Vector3 _pivot = new Vector3(0, 0, 0.5f);

    public bool IsJump(GameObject go)
    {
        Vector3 origin = go.transform.position + _pivot;
        Vector3 forward = go.transform.position + Vector3.forward + _pivot;

        Debug.DrawLine(origin, forward);
        if (Physics.Raycast(origin, forward, _rayDistance, LayerMask.NameToLayer("Obstacle")))
        {
            return true;
        }
        return false;
    }

    private float _timer = 0;
    private const float _count = 3f;
    public bool IsSkill()
    {
        _timer += Time.deltaTime;
        if(_timer >= _count)
        {
            _timer = 0;
            return true;
        }
        return false;
    }
}
