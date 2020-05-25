using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinLock : MonoBehaviour
{

    public bool isLocked = true;
    //public bool isBought = false;
    public bool isSelected = false;


    public GameObject locker;
    public GameObject priceTxt;

    public GameObject buttonBuy;
    public GameObject buttonBuyShadow;

    public int price = 300;

    // Start is called before the first frame update
    void Awake()
    {
        isLocked = true;

        if(locker != null)
        {
            locker.SetActive(true);
        }

        if(priceTxt != null)
        {
            priceTxt.SetActive(true);
        }

        GetComponent<Image>().color = !isLocked ? Color.white : Color.grey;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
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
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Selector"))
        {
            isSelected = false;
        }
    }

    public void SaveSkin(bool[] skins)
    {
        SaveSystem.SaveSkin(skins);
    }

    public void LoadSkin(int id)
    {

        DataScript data = SaveSystem.LoadSkin();

        isLocked = data.isLocked[id];

        GetComponent<Image>().color = !isLocked ? Color.white : Color.grey;
    }

}
