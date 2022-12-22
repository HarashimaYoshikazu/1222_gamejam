using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotageSkill :MonoBehaviour, ISkill
{
    private CharaController[] players;
    private CharaController player;

    private void Start()
    {
        player = GetComponent<CharaController>();
        players = FindObjectsOfType<CharaController>();
    }

    public void UseSkill(ControllType controllType)
    {
        foreach(CharaController c in players)
        {
            if(c != player)
            {
                c.SetPosition(-2);
            }
        }
    }
}
