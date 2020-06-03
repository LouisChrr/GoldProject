using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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

    public int bigBossID;

    private bool[] skinsLocked;
    private bool[] skinEquipped;

    public int boughtSkins;

    // Start is called before the first frame update
    void Start()
    {
        
        LoadMoney();
        SaveSystem.LoadSkin();
        LoadSkinsBought();
        imageSkin = GetComponent<Image>();
        buyUI.SetActive(false);
        impossibleToBuy.SetActive(false);


        //Debug.Log(skins[0].GetComponent<SkinLock>().isLocked);

        bigBossID = skins.Length - 1;

        //moneyTxt.GetComponent<Text>().text = "Gold: " + money;
        //moneyTxt.GetComponent<Text>().text = "Gold: " + FindObjectOfType<ScoreManager>().PlayerMoney;

        skinsLocked = new bool[skins.Length];
        skinsLocked = SaveSystem.LoadSkin().isLocked;

        skinEquipped = new bool[skins.Length];
        skinEquipped = SaveSystem.LoadSkin().isEquipped;

        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].GetComponent<SkinLock>().isLocked = skinsLocked[i];
            skins[i].GetComponent<SkinLock>().isEquipped = skinEquipped[i];

            skins[0].GetComponent<SkinLock>().isLocked = false;
            skins[0].GetComponent<SkinLock>().isEquipped = true;
            skins[1].GetComponent<SkinLock>().isLocked = false;

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
        //moneyTxt.GetComponent<TextMeshProUGUI>().text = "" + FindObjectOfType<ScoreManager>().PlayerMoney;

        if (!skins[bigBossID].GetComponent<SkinLock>().isLocked)
        {
            UIScript.Instance.UnlockedBigBoss();
        }
    }

    public void ChooseSkin()
    {
        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].GetComponent<SkinLock>().isEquipped = false;
            skinEquipped[i] = false;

            if (skins[i].GetComponent<SkinLock>().isSelected && !skins[i].GetComponent<SkinLock>().isLocked)
            {
                imageSkin.sprite = skins[i].GetComponent<SkinObject>().spriteSkin;
                spritePlayer = imageSkin.sprite;
                skins[i].GetComponent<SkinLock>().isEquipped = true;
                skinEquipped[i] = true;
            }

            SaveSystem.SaveSkin(skinsLocked, skinEquipped);

        }
    }

    public void BuySkin()
    {
        for(int i = 0; i < skins.Length; i++)
        {
            if (skins[i].GetComponent<SkinLock>().isSelected && skins[i].GetComponent<SkinLock>().isLocked)
            {
                buyText.GetComponent<Text>().text = "Would you like to buy this \n skin for " + skins[i].GetComponent<SkinLock>().price + " \n gold ? ";
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
                SaveSystem.SaveSkin(skinsLocked, skinEquipped);

                boughtSkins++;
                SaveSkinsBought();

                if (boughtSkins <= 4)
                {
                    UIScript.Instance.SkinAchievementIncrement1();
                }

                if (boughtSkins <= 8)
                {
                    UIScript.Instance.SkinAchievementIncrement2();
                }

                if (boughtSkins <= 16)
                {
                    UIScript.Instance.SkinAchievementIncrement3();
                }

            }
            else if (skins[i].GetComponent<SkinLock>().isSelected && skins[i].GetComponent<SkinLock>().price >= /*money*/ FindObjectOfType<ScoreManager>().PlayerMoney)
            {
                buyUI.SetActive(false);
                impossibleToBuy.SetActive(true);
            }
        }

    }

    public void NoBuy()
    {
        buyUI.SetActive(false);
        impossibleToBuy.SetActive(false);
    }

    public void SaveMoney()
    {
        SaveSystem.SaveMoney(ScoreManager.Instance);
    }

    public void LoadMoney()
    {
        DataScript data = SaveSystem.LoadMoney();

        //money = data.money;
        ScoreManager.Instance.PlayerMoney = data.money;
        ScoreManager.Instance.totalMoney = data.totalMoney;
    }

    public void SaveSkinsBought()
    {
        SaveSystem.SaveBoughtSkins(this);
    }

    public void LoadSkinsBought()
    {
        DataScript data = SaveSystem.LoadBoughtSkins();

        boughtSkins = data.bougthSkins;
    }

}
