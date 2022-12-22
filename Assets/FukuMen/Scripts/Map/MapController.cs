using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    float _speed = 2f;

    Vector3 _resetPosition;
    float _outPositionZ;
    public void Init(Vector3 resetPos,float outPosZ)
    {
        _resetPosition= resetPos;
        _outPositionZ= outPosZ;
    }

    public void OnUpdaete()
    {
        Move();
    }
    private void Move()
    {
        transform.Translate(new Vector3(0, 0, -_speed *Time.deltaTime));
        if (this.transform.position.z<=_outPositionZ)
        { 
            ResetPostion();
        }
    }

    void ResetPostion()
    {
        this.transform.position = _resetPosition;
    }
}
