using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public static LayerManager Instance;
    public List<SpriteRenderer> AllActiveSprites = new List<SpriteRenderer>();
    public int tickrate;
    private int timer;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("LAYERMANGEERR ??????");
            return;
        }
        Instance = this;

        AllActiveSprites.Clear();
    }

    void Update()
    {
        //UpdSetCurrentDepth();
    }

    public void SetCurrentDepth()
    {
        // Jappelle ca a chaque spawn de cerlce (dans Circle.cs) pour plus d'effienciy car on se sent developpeur
        // Mais si ca ne va pas remettre la fonction Update avec la tickrate sa marche bien wallah c opti aussi
        // ps: et si on updatait seulement le layer du circle qu'on vient de spawn ?
        // je me rappelle plus pk on update tout à la fois hihi

           // print("sprites update!");
            timer = 0;

            // Store les sprites nulls (wtf les amis)
            List<SpriteRenderer> nullSpritesForWhateverReasonsLol = new List<SpriteRenderer>();
            foreach (SpriteRenderer sprite in AllActiveSprites)
            {
                if (sprite != null)
                {
                    if (sprite.material.name != "UltimeMat")
                    {
                        float currentSpriteDepth = sprite.transform.position.z;
                        ChangeLayer(sprite, currentSpriteDepth);
                    }
                    else
                    {
                        float currentSpriteDepth = sprite.transform.position.z;
                        ChangeLayer(sprite, currentSpriteDepth - 1);
                    }
                }
                else
                {
                    nullSpritesForWhateverReasonsLol.Add(sprite);
                }
            }

            // Clear des sprites nulls lol wtf
            foreach (SpriteRenderer sprite in nullSpritesForWhateverReasonsLol)
            {
                AllActiveSprites.Remove(sprite);
            }
        

    }

    void UpdSetCurrentDepth()
    {
        timer += 1;
        if(timer % tickrate == 0)
        {
            print("sprites update!");
            timer = 0;

            // Store les sprites nulls (wtf les amis)
            List<SpriteRenderer> nullSpritesForWhateverReasonsLol = new List<SpriteRenderer>();
            foreach (SpriteRenderer sprite in AllActiveSprites)
            {
                if (sprite != null)
                {
                    if (sprite.material.name != "UltimeMat")
                    {
                        float currentSpriteDepth = sprite.transform.position.z;
                        ChangeLayer(sprite, currentSpriteDepth);
                    }
                    else
                    {
                        float currentSpriteDepth = sprite.transform.position.z;
                        ChangeLayer(sprite, currentSpriteDepth - 1);
                    }
                }
                else
                {
                    nullSpritesForWhateverReasonsLol.Add(sprite);
                }
            }

            // Clear des sprites nulls lol wtf
            foreach(SpriteRenderer sprite in nullSpritesForWhateverReasonsLol)
            {
                AllActiveSprites.Remove(sprite);
            }
        }
        
    }

    void ChangeLayer(SpriteRenderer sprite, float newDepth)
    {
        sprite.sortingOrder = -Mathf.RoundToInt(newDepth);
    }
}