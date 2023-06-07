using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;


public class UIManager : MonoBehaviour
{
    #region BOARD
    [Tooltip("설명서")]
    [SerializeField] GameObject stateBoard;
    [SerializeField] GameObject helpBoard;
    [SerializeField] GameObject movePG;
    [SerializeField] GameObject uiPG;
    [SerializeField] Image movebtt;
    [SerializeField] Image uibtt;
    #endregion
    #region DESTROY_NOT
    public Canvas c1, c2;
    public Camera cam1;
    public GameObject eve1;
    //맵 로고
    [Tooltip("맵 로고")]
    [SerializeField] CanvasGroup mapLogo;
    [SerializeField] Text LogoText;
    [SerializeField] TextMeshProUGUI nameText;

    //루팅 UI
    [Tooltip("루팅 UI")]
    [SerializeField] GameObject RootBoard;
    [SerializeField] Image rootImage;
    [SerializeField] TextMeshProUGUI rootName;

    #endregion

    #region SLOT
    [SerializeField] private ItemData[] itemData = new ItemData[2];
    [SerializeField] private Button[] Buttons = new Button[2];
    [SerializeField] SpriteAtlas spAt;
    [SerializeField] private TextMeshProUGUI InfoText;

    public TextMeshProUGUI ADText, ASText, DEFText;
    public TextMeshProUGUI PHpText, PADText, PASText, PDEFText;

    public Image[] InvenSlots = new Image[2];
    public Image[] Slots = new Image[2];


    [SerializeField] float AD, AS, DEF;
    int slot = 0;
    Color color = new Color(255, 255, 255, 255);
    #endregion

    #region PLAYER_INFO
    public PlayerStat playerStat;
    #endregion


    bool stateCheck, helpCheck;

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
        DontDestroyOnLoad(mapLogo);
        DontDestroyOnLoad(stateBoard);
        DontDestroyOnLoad(RootBoard);
    }

    void Awake()
    {
        #region SETUP
        DontDestroy();
        Fade(mapLogo);
        #endregion
        var data = GameObject.FindObjectOfType<PlayerControl>().returnStat();
        playerStat = data;
        if (playerStat == null)
        {
            Debug.Log("ㅈ됨");
        }
        Init();
        stateCheck = false;
    }

    public void Fade(CanvasGroup fade)
    {

        StopAllCoroutines();
        fade.alpha = 0f;
        StartCoroutine(FadeIn(fade));
        StartCoroutine(FadeOut(fade));
    }

    public void stateclick()
    {
        stateCheck = !stateCheck;
        stateBoard.SetActive(stateCheck);
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

    public void helpClick()
    {
        helpCheck = !helpCheck;
        helpBoard.SetActive(helpCheck);
    }
    public void helpButtonClick(int num)
    {
        switch (num)
        {
            case 1:
                Color color = uibtt.color;
                color.a = 0.5f;
                uibtt.color = color;

                color = movebtt.color;
                color.a = 1f;
                movebtt.color = color;

                uiPG.SetActive(false);
                movePG.SetActive(true);
                break;
            case 2:
                Color colors = movebtt.color;
                colors.a = 0.5f;
                movebtt.color = colors;

                colors = uibtt.color;
                colors.a = 1f;
                uibtt.color = colors;

                movePG.SetActive(false);
                uiPG.SetActive(true);
                break;
        }
    }

    IEnumerator FadeIn(CanvasGroup fade)
    {
        yield return new WaitForSeconds(0.2f);
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            fade.alpha = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        fade.alpha = 1f;

    }
    IEnumerator FadeOut(CanvasGroup fade)
    {
        yield return new WaitForSeconds(3.0f);
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            fade.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        fade.alpha = 0f;
    }
    IEnumerator RootOnOff()
    {
        RootBoard.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        RootBoard.SetActive(false);
    }
    //플레이어 인벤토리에 있는 정보 가져와 UI에 입력
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

    public void RootUI(int slotnum)
    {
        rootImage.sprite = spAt.GetSprite(itemData[slotnum].spriteName);
        rootName.text = itemData[slotnum].Name.ToString();
        StopCoroutine(RootOnOff());
        StartCoroutine(RootOnOff());

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
        PASText.text = (playerStat.AS + AS).ToString();
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

    public void ChangeStageLogo(string text)
    {
        LogoText.text = text.ToString();
        Fade(mapLogo);
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
