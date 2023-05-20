using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


public class PlayerInven : MonoBehaviour
{
    ItemData[] itemData = new ItemData[2];

    public int[] id = new int[2];
    int slot = 0;
    public Image[] s = new Image[2];
    [SerializeField]SpriteAtlas spAt;
    Color color;

    public Image[] InvenImage = new Image[2];

    enum Type { weapon, coin, objects, dropItem, unknown };

    public void PickItem(int itemID, GameObject touch)
    {
        if (slot >= 2) { Debug.Log("Full"); return; }

        color = new Color(255, 255, 255, 255);
        

        touch.SetActive(false);
        DataManager.Instance().LoadDatas();
        var data = DataManager.Instance().dicItemData[itemID];
        Debug.Log("PickUP : " + data.Name);
        id[slot] = data.Id;
        //Color color = s[slot].GetComponent<Color>();
        //color.a = 1f;
        s[slot].sprite = spAt.GetSprite(data.spriteName);
        s[slot].color = color;
        InvenImage[slot].sprite = s[slot].sprite;
        InvenImage[slot].color = color;
        slot++;

    }

}