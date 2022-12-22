using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    float _speed = 2f;

    public void OnUpdaete()
    {

        Move();

    }
    private void Move()
    {
        transform.Translate(new Vector3(0, 0, -_speed *Time.deltaTime));
    }
}
