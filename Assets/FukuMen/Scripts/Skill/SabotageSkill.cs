using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotageSkill :MonoBehaviour, ISkill
{
    private CharaController[] players;

    private void Start()
    {
        players = FindObjectsOfType<CharaController>();
    }

    public void UseSkill()
    {
        foreach(CharaController c in players)
        {
            if(c._controllType == ControllType.CPU)
            {
                c.SetPosition(-2);
            }
        }
    }
}
