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
        private bool allLoaded;                                 // ����� �ҷ����� �۾��� �Ϸ� �Ǿ�����
        private bool loadComplete;                              // ���� ���̽��� �ε� �Ǿ�����
        private IntroPhase introPhase = IntroPhase.Start;       // ���� �ε� ������
        private Coroutine loadGaugeUpdateCoroutine;             // �ε��ٿ� �������� �ٷ�� �ڷ�ƾ

        /// <summary>
        /// �ܺο��� LoadComplete�� �����ϱ� ���� ������Ƽ
        /// �߰��� ���� ������ �Ϸ� �� ���ǿ� ���� ���� ������� ����
        /// </summary>
        public bool LoadComplete
        {
            // ���� ������ �Ϸῡ ���� �������� ��ȯ
            get => loadComplete;

            set
            {
                loadComplete = value;

                if (loadComplete && !allLoaded)
                {
                    // ��� ����� �Ϸ� �Ǿ��ٸ� ���� ���� ȣ��
                    NextPhase();
                }
            }
        }

        /// <summary>
        /// �ʱ� ������ �����ϴ� �޼���
        /// </summary>
        public void Initialize()
        {
            // ������ ������ ����
            OnPhase(introPhase);
        }

        /// <summary>
        /// ����� ���� ������ �����ϴ� �޼���
        /// </summary>
        /// <param name="phase"></param>
        private void OnPhase(IntroPhase phase)
        {
            // ���� ���� UI ����
            uiStart.SetLoadStateDescription(phase.ToString());

            if (loadGaugeUpdateCoroutine != null)
            {
                // ���� �������� �ٷ�� �ڷ�ƾ�� ���� ���ε� �� ����Ǹ� �ȵǹǷ�
                // �ϴ� ���߰� ����ش�
                StopCoroutine(loadGaugeUpdateCoroutine);
                loadGaugeUpdateCoroutine = null;
            }

            if (phase != IntroPhase.Complete)
            {
                // �������� ������ �ʾҴٸ� ���� �ۼ�Ʈ�� �ڷ�ƾ���� �����ش�
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
        /// ���� ������� �����ϴ� �޼���
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