using MaidEscape.Define;
using MaidEscape.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaidEscape
{

    public class StartController : MonoBehaviour
    {
        // Public
        public UIStart uiStart;

        // Private
        private bool allLoaded;                                 // 페이즈를 불러오는 작업이 완료 되었는지
        private bool loadComplete;                              // 현재 페이스가 로드 되었는지
        private IntroPhase introPhase = IntroPhase.Start;       // 현재 로딩 페이즈
        private Coroutine loadGaugeUpdateCoroutine;             // 로딩바에 게이지를 다루는 코루틴

        /// <summary>
        /// 외부에서 LoadComplete에 접근하기 위한 프로퍼티
        /// 추가로 현재 페이즈 완료 시 조건에 따라 다음 페이즈로 변경
        /// </summary>
        public bool LoadComplete
        {
            // 현재 페이즈 완료에 대한 진리값을 반환
            get => loadComplete;

            set
            {
                loadComplete = value;

                if (loadComplete && !allLoaded)
                {
                    // 모든 페이즈가 완료 되었다면 다음 씬을 호출
                    NextPhase();
                }
            }
        }

        /// <summary>
        /// 초기 세팅을 실행하는 메서드
        /// </summary>
        public void Initialize()
        {
            // 페이즈 세팅을 실행
            OnPhase(introPhase);
        }

        /// <summary>
        /// 페이즈에 대한 로직을 실행하는 메서드
        /// </summary>
        /// <param name="phase"></param>
        private void OnPhase(IntroPhase phase)
        {
            // 진행 상태 UI 세팅
            uiStart.SetLoadStateDescription(phase.ToString());

            if (loadGaugeUpdateCoroutine != null)
            {
                // 진행 게이지를 다루는 코루틴이 실행 중인데 또 실행되면 안되므로
                // 일단 멈추고 비워준다
                StopCoroutine(loadGaugeUpdateCoroutine);
                loadGaugeUpdateCoroutine = null;
            }

            if (phase != IntroPhase.Complete)
            {
                // 페이지가 끝나지 않았다면 진행 퍼센트를 코루틴으로 보여준다
                var loadPer = (float)phase / (float)IntroPhase.Complete;
                loadGaugeUpdateCoroutine = StartCoroutine(uiStart.LoadGaugeUpdate(loadPer));
            }
            else
            {
                uiStart.loadFillGauge.fillAmount = 1;
            }

            switch (phase)
            {
                case IntroPhase.Start:
                    LoadComplete = true;
                    break;

                case IntroPhase.ApplicationSetting:
                    LoadComplete = true;
                    break;

                case IntroPhase.Server:
                    LoadComplete = true;
                    break;

                case IntroPhase.StaticData:
                    LoadComplete = true;
                    break;

                case IntroPhase.UserData:
                    LoadComplete = true;
                    break;

                case IntroPhase.Resource:
                    LoadComplete = true;
                    break;

                case IntroPhase.UI:
                    LoadComplete = true;
                    break;

                case IntroPhase.Complete:
                    SceneManager.LoadScene(SceneType.Story.ToString());
                    allLoaded = true;
                    LoadComplete = true;
                    break;
            }
        }

        /// <summary>
        /// 다음 페이즈로 변경하는 메서드
        /// </summary>
        private void NextPhase()
        {
            StartCoroutine(WaitForSeconds());

            IEnumerator WaitForSeconds()
            {
                yield return new WaitForSeconds(.5f);
                LoadComplete = false;
                OnPhase(++introPhase);
            }
        }
    }
}