using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : Singleton<CreateCharacter>
{
    [SerializeField]
    CharaController _almondPrefab;
    [SerializeField]
    CharaController _coinPrefab;
    [SerializeField]
    CharaController _eggPrefab;

    CharacterType _selectedType;

    [SerializeField]
    Transform _playerPosition;
    [SerializeField]
    Transform[] _othersPosition;

    public void SelectCharacter(CharacterType type)
    {
        _selectedType = type;
    }

    public CharaController[] Create()
    {
        CharaController[] charaControllers = new CharaController[3];
        switch (_selectedType)
        {
            case CharacterType.Almond:
                charaControllers[0] = Instantiate(_almondPrefab,_playerPosition.position,Quaternion.identity);
                charaControllers[1] = Instantiate(_eggPrefab, _othersPosition[0].position, Quaternion.identity);
                charaControllers[2] = Instantiate(_coinPrefab, _othersPosition[1].position, Quaternion.identity);
                charaControllers[1].ControllType = ControllType.CPU;
                charaControllers[2].ControllType = ControllType.CPU;

                break;
            case CharacterType.Coin:
                charaControllers[0] = Instantiate(_coinPrefab, _playerPosition.position, Quaternion.identity);
                charaControllers[1] = Instantiate(_eggPrefab, _othersPosition[0].position, Quaternion.identity);
                charaControllers[2] = Instantiate(_almondPrefab, _othersPosition[1].position, Quaternion.identity);
                charaControllers[1].ControllType = ControllType.CPU;
                charaControllers[2].ControllType = ControllType.CPU;
                break;
            case CharacterType.Egg:
                charaControllers[0] = Instantiate(_eggPrefab, _playerPosition.position, Quaternion.identity);
                charaControllers[1] = Instantiate(_almondPrefab, _othersPosition[0].position, Quaternion.identity);
                charaControllers[2] = Instantiate(_coinPrefab, _othersPosition[1].position, Quaternion.identity);
                charaControllers[1].ControllType = ControllType.CPU;
                charaControllers[2].ControllType = ControllType.CPU;
                break;
        }

        return charaControllers; ;
    }
}
