using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public static LayerManager Instance;
    public List<SpriteRenderer> AllActiveSprites = new List<SpriteRenderer>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("LAYERMANGEERR ??????");
            return;
        }
        Instance = this;
    }

    void Update()
    {
        SetCurrentDepth();
    }

    void SetCurrentDepth()
    {
        foreach (SpriteRenderer sprite in AllActiveSprites)
        {
            float currentSpriteDepth = sprite.transform.position.z;
            ChangeLayer(sprite, currentSpriteDepth);
        }
    }

    void ChangeLayer(SpriteRenderer sprite, float newDepth)
    {
        sprite.sortingOrder = -Mathf.RoundToInt(newDepth);
    }
}