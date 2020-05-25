using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinObject : MonoBehaviour
{
    public bool isMainSkin;
    public int spriteIndex = 0;

    private Material thisMat;

    public List<BilleSprites> allSkins = new List<BilleSprites>();

    public Sprite spriteSkin { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (!isMainSkin)
            spriteSkin = GetComponent<Image>().sprite;

        if (isMainSkin)
        {
            if(SkinMenu.spritePlayer != null)
            {
                GetComponent<SpriteRenderer>().material.SetTexture("_Skin", SkinMenu.spritePlayer.texture);
                // GetComponent<SpriteRenderer>().sprite = SkinMenu.spritePlayer;

                StartCoroutine(SwitchSprite());

                thisMat = GetComponent<SpriteRenderer>().material;
            }
        }
    }

    private BilleSprites GetSkinAnim()
    {
        foreach (BilleSprites bSprites in allSkins)
        {
            if (bSprites.Front == SkinMenu.spritePlayer)
                return bSprites;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SwitchSprite()
    {
        yield return new WaitForSeconds(0.2f);

        if (spriteIndex == 0)
            spriteIndex = 1;
        else
            spriteIndex = 0;

        thisMat.SetTexture("_Skin", GetSkinAnim().Back[spriteIndex].texture);

        StartCoroutine(SwitchSprite());
    }
}