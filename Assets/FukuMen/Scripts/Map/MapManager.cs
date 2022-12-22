using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    bool _isPause = false;

    [SerializeField]
    List<MapController> _maps;

    [SerializeField]
    Transform _resetPositionTransform;
    [SerializeField]
    float _outPositionZ;

    private void Start()
    {
        foreach (var map in _maps)
        {
            map.Init(_resetPositionTransform.position,_outPositionZ);
        }
    }
    void Update()
    {
        if (_isPause)
        {
            return;
        }
        foreach(var map in _maps)
        {
            map.OnUpdaete();
        }
    }
}
