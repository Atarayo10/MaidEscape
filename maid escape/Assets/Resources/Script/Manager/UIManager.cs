using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;

public class UIManager:MonoBehaviour
{
    [SerializeField] GameObject stateBoard;

    #region SLOT
    [SerializeField]private ItemData[] itemData = new ItemData[2];
    [SerializeField]private Button[] Buttons = new Button[2];
    [SerializeField]SpriteAtlas spAt;
    [SerializeField]private TextMeshProUGUI InfoText;
    public TextMeshProUGUI ADText, ASText, DEFText;

    public Image[] InvenSlots = new Image[2];
    public Image[] Slots = new Image[2];


    [SerializeField]float AD, AS, DEF;
    int slot = 0;
    Color color = new Color(255, 255, 255, 255);
    #endregion

    [SerializeField] CanvasGroup townLogo;
    bool check;

    public float fadeTime = 1f; // 페이드 타임 
    float accumTime = 0f;

    public static UIManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        StopAllCoroutines();
        townLogo.alpha = 0f;
        StartCoroutine(FadeIn());
        StartCoroutine(FadeOut());

        Init();
        check = false;
    }

    public void stateclick()
    {
        check = !check;
        stateBoard.SetActive(check);
        if (itemData[0] == null)
        {
            InfoText.text = " 아무것도 없네욤.....";

        }
        else
        {
            InfoText.text = itemData[0].Info.ToString();
        }


    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.2f);
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            townLogo.alpha = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        townLogo.alpha = 1f;

    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(3.0f);
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            townLogo.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        townLogo.alpha = 0f;
    }

    public void InsertItem(int num, int slotnum)
    {
        DataManager.Instance().LoadDatas();
        var data = DataManager.Instance().dicItemData[num];

        #region DATA_INSERT
        itemData[slotnum] = data;
        #endregion

        Slots[slotnum].sprite = this.spAt.GetSprite(data.spriteName);
        Slots[slotnum].color = color;
        InvenSlots[slotnum].sprite = Slots[slotnum].sprite;
        InvenSlots[slotnum].color = color;
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

    public void ChangeStat(int num)
    {
        AD = itemData[num].AD;
        AS = itemData[num].AS;
        DEF = itemData[num].DEF;
    }

    public void ChangeInFo(int num)
    {
        if(itemData[num] == null)
        {
            InfoText.text = " 아무것도 없네욤.....";
            ADText.text = 0.ToString();
            ASText.text = 0.ToString();
            DEFText.text = 0.ToString();
            return;
        }
        ChangeStat(num);
        InfoText.text = itemData[num].Info.ToString();
        ADText.text = AD.ToString();
        ASText.text = AS.ToString();
        DEFText.text = DEF.ToString();
    }


    void Init()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
