using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinObject : MonoBehaviour
{
    public bool isMainSkin;

    public Sprite spriteSkin { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        spriteSkin = GetComponent<Image>().sprite;

        if (isMainSkin)
        {
            if(SkinMenu.spritePlayer != null)
            {
                GetComponent<Image>().sprite = SkinMenu.spritePlayer;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
