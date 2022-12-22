using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    bool _isPause = false;

    [SerializeField]
    List<MapController> _maps;
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
