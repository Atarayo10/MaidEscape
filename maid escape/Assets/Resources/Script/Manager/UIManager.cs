using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class UIManager:MonoBehaviour
{
    [SerializeField] GameObject stateBoard;

    [SerializeField] CanvasGroup townLogo;
    float fadeSpeed = 0.1f;
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

    //public void PickItem(int itemID, GameObject touch)
    //{
    //    if (slot >= 2) { Debug.Log("Full"); return; }

    //    touch.SetActive(false);
    //    DataManager.Instance().LoadDatas();
    //    var data = DataManager.Instance().dicItemData[itemID];
    //    Debug.Log("PickUP : " + data.Name);
    //    id[slot] = data.Id;
    //    //Color color = s[slot].GetComponent<Color>();
    //    //color.a = 1f;
    //    s[slot].sprite = spAt.GetSprite(data.spriteName);
    //    slot++;

    //}


    void Init()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
