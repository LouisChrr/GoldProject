﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataScript
{

    public bool[] isLocked /*= new bool[6]*/;

    public bool[] isEquipped;

    public float money;

    public float bestScore;


    public DataScript(bool[] skins, bool[] skinsEquipped)
    {
        isLocked = new bool[skins.Length];
        isEquipped = new bool[skinsEquipped.Length];

        for(int i = 0; i < skins.Length; i++)
        {
            isLocked[i] = skins[i];
        }

        for (int i = 0; i < skinsEquipped.Length; i++)
        {
            isEquipped[i] = skinsEquipped[i];
        }

    }

    public DataScript()
    {
        isLocked = new bool[17];
        isEquipped = new bool[17];

        for (int i = 0; i < isLocked.Length; i++)
        {
            isLocked[i] = true;
        }

        for (int i = 0; i < isEquipped.Length; i++)
        {
            isEquipped[i] = false;
        }

    }

    public DataScript(ScoreManager player)
    {

        money = player.PlayerMoney;
        if(money >= 9999)
        {
            money = 9999;
        }
    }

    public DataScript(MenuManager player)
    {

        bestScore = player.PlayerBestScore;
    }
}

