﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float CoinSpawnChance;
    public Transform Coins;
    public GameObject _CoinPrefab;
    public float Yoffset;
    public static CoinSpawner Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("2 CoinSpawner??");
            return;
        }
        Instance = this;
    }


    public void SpawnCoin()
    {
        if(Random.Range(0,100) < CoinSpawnChance*100) 
        {
            Vector3 SpawnPos = new Vector3(0, Yoffset, GameManager.Instance.CirclesNumber);
            GameObject InstantiatedCoin = Instantiate(_CoinPrefab, SpawnPos, Quaternion.Euler(0, 0, 180), Coins);
        }
    }


}
