using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkinMenu : MonoBehaviour
{
    public static Sprite spritePlayer;

    private Image imageSkin;

    public GameObject buyUI;
    public GameObject buyText;

    public GameObject moneyTxt;
    //public int money;

    public GameObject impossibleToBuy;
    
    public GameObject[] skins;

    private bool[] skinsLocked;

    // Start is called before the first frame update
    void Start()
    {
        
        LoadMoney();
        imageSkin = GetComponent<Image>();
        buyUI.SetActive(false);

        //moneyTxt.GetComponent<Text>().text = "Gold: " + money;
        //moneyTxt.GetComponent<Text>().text = "Gold: " + FindObjectOfType<ScoreManager>().PlayerMoney;

        impossibleToBuy.SetActive(false);

        skinsLocked = new bool[skins.Length];
        skinsLocked = SaveSystem.LoadSkin().isLocked;

        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].GetComponent<SkinLock>().isLocked = skinsLocked[i];

            if (!skins[i].GetComponent<SkinLock>().isLocked)
            {
                skins[i].GetComponent<Image>().color = Color.white;
                skins[i].GetComponent<SkinLock>().locker.SetActive(false);
                skins[i].GetComponent<SkinLock>().priceTxt.SetActive(false);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        //moneyTxt.GetComponent<Text>().text = "Gold: " + money;
        moneyTxt.GetComponent<Text>().text = "" + FindObjectOfType<ScoreManager>().PlayerMoney;
    }

    public void ChooseSkin()
    {
        for (int i = 0; i < skins.Length; i++)
        {
            if (skins[i].GetComponent<SkinLock>().isSelected && !skins[i].GetComponent<SkinLock>().isLocked)
            {
                imageSkin.sprite = skins[i].GetComponent<SkinObject>().spriteSkin;
                spritePlayer = imageSkin.sprite;
            }
        }
    }

    public void BuySkin()
    {
        for(int i = 0; i < skins.Length; i++)
        {
            if (skins[i].GetComponent<SkinLock>().isSelected && skins[i].GetComponent<SkinLock>().isLocked)
            {
                buyText.GetComponent<Text>().text = "Would you like to buy this skin \n for " + skins[i].GetComponent<SkinLock>().price + " \n gold ? ";
                buyUI.SetActive(true);
            }
        }
    }

    public void YesBuy()
    {
        for (int i = 0; i < skins.Length; i++)
        {
            if (skins[i].GetComponent<SkinLock>().isSelected && skins[i].GetComponent<SkinLock>().price <= /*money*/ FindObjectOfType<ScoreManager>().PlayerMoney)
            {
                skins[i].GetComponent<SkinLock>().isLocked = false;
                skins[i].GetComponent<SkinLock>().locker.SetActive(false);
                skins[i].GetComponent<SkinLock>().priceTxt.SetActive(false);

                //money -= skins[i].GetComponent<SkinLock>().price;
                FindObjectOfType<ScoreManager>().PlayerMoney -= skins[i].GetComponent<SkinLock>().price;
                SaveMoney();

                buyUI.SetActive(false);

                skins[i].GetComponent<Image>().color = Color.white;
                skinsLocked[i] = false;

            }
            else if (skins[i].GetComponent<SkinLock>().isSelected && skins[i].GetComponent<SkinLock>().price >= /*money*/ FindObjectOfType<ScoreManager>().PlayerMoney)
            {
                impossibleToBuy.SetActive(true);
            }
        }

        SaveSystem.SaveSkin(skinsLocked);
    }

    public void NoBuy()
    {
        buyUI.SetActive(false);
        impossibleToBuy.SetActive(false);
    }

    public void SaveMoney()
    {
        SaveSystem.SaveMoney(FindObjectOfType<ScoreManager>());
    }

    public void LoadMoney()
    {
        DataScript data = SaveSystem.LoadMoney();

        //money = data.money;
        ScoreManager.Instance.PlayerMoney = data.money;

    }

}
