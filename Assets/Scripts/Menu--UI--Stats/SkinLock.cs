using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinLock : MonoBehaviour
{

    public bool isLocked = true;
    public bool isEquipped = false;
    public bool isSelected = false;


    public GameObject locker;
    public GameObject priceTxt;

    public GameObject buttonBuy;
    public GameObject buttonBuyShadow;

    public GameObject buttonEquip;
    public GameObject buttonEquipShadow;

    public int price = 300;
    

    void Awake()
    {
        isLocked = true;

        if (locker != null)
        {
            locker.SetActive(true);
        }

        if(priceTxt != null)
        {
            priceTxt.SetActive(true);
        }

        GetComponent<Image>().color = !isLocked ? Color.white : Color.grey;

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Selector")) {

            isSelected = true;
            
            if (isLocked)
            {
                buttonBuy.GetComponent<Image>().color = Color.white;
                buttonBuyShadow.GetComponent<Image>().color = Color.white;
            }
            else
            {
                buttonBuy.GetComponent<Image>().color = Color.grey;
                buttonBuyShadow.GetComponent<Image>().color = Color.grey;
            }

            if (isEquipped)
            {
                buttonEquip.GetComponent<Image>().color = Color.grey;
                buttonEquipShadow.GetComponent<Image>().color = Color.grey;
            }
            else
            {
                buttonEquip.GetComponent<Image>().color = Color.white;
                buttonEquipShadow.GetComponent<Image>().color = Color.white;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Selector"))
        {
            isSelected = false;
        }
    }

    public void SaveSkin(bool[] skins, bool[] skinsEquipped)
    {
        SaveSystem.SaveSkin(skins, skinsEquipped);
    }

    public void LoadSkin(int id)
    {

        DataScript data = SaveSystem.LoadSkin();

        isLocked = data.isLocked[id];
        isEquipped = data.isEquipped[id];

        GetComponent<Image>().color = !isLocked ? Color.white : Color.grey;
    }

}
