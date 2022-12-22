using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : Singleton<CreateCharacter>
{
    [SerializeField]
    SelectButton[] _selectButtons;
    [SerializeField]
    GameObject[] _characterPrefabs;

    CharaController[] _charaControllers;

    private void Start()
    {

    }

    public CharaController[] Create()
    {
        _charaControllers = new CharaController[_characterPrefabs.Length];
        for (int i = 0; i < _characterPrefabs.Length; i++)
        {
            _charaControllers[i] = Instantiate(_characterPrefabs[i]).GetComponent<CharaController>();           
        }
        return _charaControllers;
    }
}
