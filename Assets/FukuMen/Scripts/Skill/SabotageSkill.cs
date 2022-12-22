using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotageSkill :MonoBehaviour, ISkill
{
    [SerializeField]
    private CharaController _charaController;

    public void UseSkill()
    {
        Debug.Log("–WŠQƒXƒLƒ‹I");
    }
}
