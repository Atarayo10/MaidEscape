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
    /// 게임에 사용되는 모든 데이터를 관리하는 클래스
    /// 게임 씬 변경등과 같은 큰 흐름을 관리하기도 함
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        // Public
        [HideInInspector]
        public float loadProgress;      // 불러오는 씬의 진행상태

        // Private


        protected override void Awake()
        {
            base.Awake();

            if (gameObject == null)
            {
                return;
            }

            // 씬이 변경되더라도 파괴되면 안됨
            DontDestroyOnLoad(gameObject);

            // 현재 씬에서 초기 세팅 작업을 하는 오브젝트를 찾아 메서드 실행
            var startController = FindObjectOfType<StartController>();
            startController?.Initialize();
        }

        /// <summary>
        /// 씬 전환을 관할하는 메서드
        /// </summary>
        /// <param name="sceneType"> 불러오고자 하는 씬 타입 </param>
        /// <param name="loadCoroutine"> 씬 준비 작업 코루틴 </param>
        /// <param name="loadComplete"> 씬 로드를 마무리 하면서 실행 시킬 메서드 </param>
        public void LoadScene(SceneType sceneType, IEnumerator loadCoroutine = null, Action loadComplete = null)
        {
            StartCoroutine(WaitForLoad());


            IEnumerator WaitForLoad()
            {
                // 현재 진행상태 초기화
                loadProgress = 0;

                // 로딩 씬을 띄워놓고 작업하기 위해서 로딩 씬을 호출
                yield return SceneManager.LoadSceneAsync(SceneType.Loading.ToString());

                // 원하는 씬을 불러온 뒤에 비활성화
                var asyncOper = SceneManager.LoadSceneAsync(sceneType.ToString(), LoadSceneMode.Additive);
                asyncOper.allowSceneActivation = false;

                if (loadCoroutine != null)
                {
                    // 씬을 준비하는데 필요한 작업이 있다면 
                    // 해당 작업이 완료될 때 까지 대기
                    yield return StartCoroutine(loadCoroutine);
                }

                // 씬 호출이 끝날 때 까지 반복
                while (!asyncOper.isDone)
                {
                    if (loadProgress >= .9f)
                    {
                        // 씬 호출이 끝나면 마무리 작업
                        loadProgress = 1f;

                        yield return new WaitForSeconds(1f);

                        asyncOper.allowSceneActivation = true;
                    }
                    else
                    {
                        // 씬 로딩의 진행상황을 표시
                        loadProgress = asyncOper.progress;
                    }

                    // 코루틴 내에서 반복문 사용 시
                    // 로직을 한 번 실행 후 메인 로직을 실행 할 수 있도록
                    yield return null;
                }

                // 다음 씬을 준비 하기 위한 작업을 모두 끝냈으므로, 로딩씬 비활성화
                yield return SceneManager.UnloadSceneAsync(SceneType.Loading.ToString());

                // 준비 작업이 끝나고 난 뒤에 실행 시킬 작업이 있다면 실행
                loadComplete?.Invoke();
            }

        }

    }
}