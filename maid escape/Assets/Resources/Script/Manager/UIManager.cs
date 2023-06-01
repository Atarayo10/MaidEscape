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
    #region DESTROY_NOT
    public Canvas c1, c2;
    public Camera cam1;
    public GameObject eve1;
    [SerializeField] CanvasGroup townLogo;
    [SerializeField] Text LogoText;
    [SerializeField] TextMeshProUGUI nameText;

    #endregion

    #region SLOT
    [SerializeField]private ItemData[] itemData = new ItemData[2];
    [SerializeField]private Button[] Buttons = new Button[2];
    [SerializeField]SpriteAtlas spAt;
    [SerializeField]private TextMeshProUGUI InfoText;

    public TextMeshProUGUI ADText, ASText, DEFText;
    public TextMeshProUGUI PHpText, PADText, PASText, PDEFText;

    public Image[] InvenSlots = new Image[2];
    public Image[] Slots = new Image[2];


    [SerializeField]float AD, AS, DEF;
    int slot = 0;
    Color color = new Color(255, 255, 255, 255);
    #endregion

    #region PLAYER_INFO
    public PlayerStat playerStat;
    #endregion


    bool check;

    public float fadeTime = 1f; // 페이드 타임 
    float accumTime = 0f;
    public static UIManager instance;
    // Start is called before the first frame update


    void DontDestroy()
    {
        DontDestroyOnLoad(c1);
        DontDestroyOnLoad(c2);
        DontDestroyOnLoad(cam1);
        DontDestroyOnLoad(eve1);
        DontDestroyOnLoad(townLogo);
        DontDestroyOnLoad(stateBoard);

    }

    void Awake()
    {
        DontDestroy();
        StopAllCoroutines();
        townLogo.alpha = 0f;
        StartCoroutine(FadeIn());
        StartCoroutine(FadeOut());
        var data = GameObject.FindObjectOfType<PlayerControl>().returnStat();
        playerStat = data;
        if(playerStat == null)
        {
            Debug.Log("ㅈ됨");
        }

        Init();
        check = false;
    }

    public void Fade()
    {

        StopAllCoroutines();
        townLogo.alpha = 0f;
        StartCoroutine(FadeIn());
        StartCoroutine(FadeOut());
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
        ChangeInFo(0);

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

    //가진 아이템 스탯으로 변환 ( 인벤의 템 클릭 시 해당 아이템의 스탯으로 변환 )
    public void ChangeStat(int num)
    {
        AD = itemData[num].AD;
        AS = itemData[num].AS;
        DEF = itemData[num].DEF;
    }
    //플레이어 정보 스탯으로 텍스트들 변환
    public void PlayerStatTextSet()
    {
        PHpText.text = playerStat.HP.ToString();
        PADText.text = (playerStat.AD + AD).ToString();
        PASText.text = (playerStat.AS +AS).ToString();
        PDEFText.text = (playerStat.DEF + DEF).ToString();
    }

    // 인벤토리 내 텍스트들 해당 아이템의 스탯으로 변환
    public void ChangeInFo(int num)
    {
        if (itemData[num] == null)
        {
            PlayerStatTextSet();
            InfoText.text = " 아무것도 없네욤.....";
            nameText.text = "맨손";
            ADText.text = "0";
            ASText.text = "0";
            DEFText.text = "0";
            return;
        }
        ChangeStat(num);
        PlayerStatTextSet();
        InfoText.text = itemData[num].Info.ToString();
        nameText.text = itemData[num].Name.ToString();
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
