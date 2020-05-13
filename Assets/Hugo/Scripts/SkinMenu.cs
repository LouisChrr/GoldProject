using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkinMenu : MonoBehaviour
{
    public static Sprite spritePlayer;

    private Image imageSkin;
    private int[] id;

    // Start is called before the first frame update
    void Start()
    {

        imageSkin = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseSkin()
    {
        imageSkin.sprite = EventSystem.current.currentSelectedGameObject.GetComponent<SkinObject>().spriteSkin;
        spritePlayer = imageSkin.sprite;
    }
}
