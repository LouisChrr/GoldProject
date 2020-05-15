using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataScript
{

    public bool[] isLocked /*= new bool[6]*/;

    public int money;


    public DataScript(bool[] skins)
    {
        isLocked = new bool[skins.Length];

        for(int i = 0; i < skins.Length; i++)
        {
            isLocked[i] = skins[i];
        }
    }

    public DataScript()
    {
        isLocked = new bool[6];

        for (int i = 0; i < isLocked.Length; i++)
        {
            isLocked[i] = true;
        }
    }

    public DataScript(SkinMenu player)
    {

        money = player.money;
    }
}

