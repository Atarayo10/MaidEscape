using MaidEscape.Define;
using MaidEscape.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaidEscape
{
    /// <summary>
    /// 寃뚯엫???ъ슜?섎뒗 紐⑤뱺 ?곗씠?곕? 愿由ы븯???대옒??
    /// 寃뚯엫 ??蹂寃쎈벑怨?媛숈? ???먮쫫??愿由ы븯湲곕룄 ??
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        // Public
        [HideInInspector]
        public float loadProgress;      // 遺덈윭?ㅻ뒗 ?ъ쓽 吏꾪뻾?곹깭

        // Private


        protected override void Awake()
        {
            base.Awake();

            if (gameObject == null)
            {
                return;
            }

            // ?ъ씠 蹂寃쎈릺?붾씪???뚭눼?섎㈃ ?덈맖
            DontDestroyOnLoad(gameObject);

            // ?꾩옱 ?ъ뿉??珥덇린 ?명똿 ?묒뾽???섎뒗 ?ㅻ툕?앺듃瑜?李얠븘 硫붿꽌???ㅽ뻾
            var startController = FindObjectOfType<StartController>();
            startController?.Initialize();
        }

        /// <summary>
        /// ???꾪솚??愿?좏븯??硫붿꽌??
        /// </summary>
        /// <param name="sceneType"> 遺덈윭?ㅺ퀬???섎뒗 ?????</param>
        /// <param name="loadCoroutine"> ??以鍮??묒뾽 肄붾（??</param>
        /// <param name="loadComplete"> ??濡쒕뱶瑜?留덈Т由??섎㈃???ㅽ뻾 ?쒗궗 硫붿꽌??</param>
        public void LoadScene(SceneType sceneType, IEnumerator loadCoroutine = null, Action loadComplete = null)
        {
            StartCoroutine(WaitForLoad());


            IEnumerator WaitForLoad()
            {
                // ?꾩옱 吏꾪뻾?곹깭 珥덇린??
                loadProgress = 0;

                // 濡쒕뵫 ?ъ쓣 ?꾩썙?볤퀬 ?묒뾽?섍린 ?꾪빐??濡쒕뵫 ?ъ쓣 ?몄텧
                yield return SceneManager.LoadSceneAsync(SceneType.Loading.ToString());

                // ?먰븯???ъ쓣 遺덈윭???ㅼ뿉 鍮꾪솢?깊솕
                var asyncOper = SceneManager.LoadSceneAsync(sceneType.ToString(), LoadSceneMode.Additive);
                asyncOper.allowSceneActivation = false;

                if (loadCoroutine != null)
                {
                    // ?ъ쓣 以鍮꾪븯?붾뜲 ?꾩슂???묒뾽???덈떎硫?
                    // ?대떦 ?묒뾽???꾨즺????源뚯? ?湲?
                    yield return StartCoroutine(loadCoroutine);
                }

                // ???몄텧???앸궇 ??源뚯? 諛섎났
                while (!asyncOper.isDone)
                {
                    if (loadProgress >= .9f)
                    {
                        // ???몄텧???앸굹硫?留덈Т由??묒뾽
                        loadProgress = 1f;

                        yield return new WaitForSeconds(1f);

                        asyncOper.allowSceneActivation = true;
                    }
                    else
                    {
                        // ??濡쒕뵫??吏꾪뻾?곹솴???쒖떆
                        loadProgress = asyncOper.progress;
                    }

                    // 肄붾（???댁뿉??諛섎났臾??ъ슜 ??
                    // 濡쒖쭅????踰??ㅽ뻾 ??硫붿씤 濡쒖쭅???ㅽ뻾 ?????덈룄濡?
                    yield return null;
                }

                // ?ㅼ쓬 ?ъ쓣 以鍮??섍린 ?꾪븳 ?묒뾽??紐⑤몢 ?앸깉?쇰?濡? 濡쒕뵫??鍮꾪솢?깊솕
                yield return SceneManager.UnloadSceneAsync(SceneType.Loading.ToString());

                // 以鍮??묒뾽???앸굹怨????ㅼ뿉 ?ㅽ뻾 ?쒗궗 ?묒뾽???덈떎硫??ㅽ뻾
                loadComplete?.Invoke();
            }

        }

    }
}