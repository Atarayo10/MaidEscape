using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


public class PlayerInven : MonoBehaviour
{
    [SerializeField]private int[] itemData = new int[2];
    int slot = 0;
    UIManager uiManager;
    [SerializeField] ParticleSystem pick;
    private void Start()
    {
        uiManager = GetComponent<UIManager>();
    }

    public void PickItem(int itemID, GameObject touch)
    {
        if (slot >= 2) { Debug.Log("Slot Full"); return; }

        touch.SetActive(false);;
        pick.Play();
        itemData[slot] = itemID;
        uiManager.InsertItem(itemData[slot], slot);
        slot++;
    }   

    public int GetItemId(int num)
    {
        return itemData[slot];
    }


    //public void PickItem(int itemID, GameObject touch)
    //{
    //    if (slot >= 2) { Debug.Log("Full"); return; }

    //    color = new Color(255, 255, 255, 255);


    //    touch.SetActive(false);
    //    DataManager.Instance().LoadDatas();
    //    var data = DataManager.Instance().dicItemData[itemID];
    //    Debug.Log("PickUP : " + data.Name);
    //    id[slot] = data.Id;
    //    //Color color = s[slot].GetComponent<Color>();
    //    //color.a = 1f;
    //    s[slot].sprite = spAt.GetSprite(data.spriteName);
    //    s[slot].color = color;
    //    InvenImage[slot].sprite = s[slot].sprite;
    //    InvenImage[slot].color = color;
    //    slot++;

    //}

}