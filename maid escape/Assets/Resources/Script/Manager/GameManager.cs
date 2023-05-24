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
    /// ���ӿ� ���Ǵ� ��� �����͸� �����ϴ� Ŭ����
    /// ���� �� ������ ���� ū �帧�� �����ϱ⵵ ��
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        // Public
        [HideInInspector]
        public float loadProgress;      // �ҷ����� ���� �������

        // Private


        protected override void Awake()
        {
            base.Awake();

            if (gameObject == null)
            {
                return;
            }

            // ���� ����Ǵ��� �ı��Ǹ� �ȵ�
            DontDestroyOnLoad(gameObject);

            // ���� ������ �ʱ� ���� �۾��� �ϴ� ������Ʈ�� ã�� �޼��� ����
            var startController = FindObjectOfType<StartController>();
            startController?.Initialize();
        }

        /// <summary>
        /// �� ��ȯ�� �����ϴ� �޼���
        /// </summary>
        /// <param name="sceneType"> �ҷ������� �ϴ� �� Ÿ�� </param>
        /// <param name="loadCoroutine"> �� �غ� �۾� �ڷ�ƾ </param>
        /// <param name="loadComplete"> �� �ε带 ������ �ϸ鼭 ���� ��ų �޼��� </param>
        public void LoadScene(SceneType sceneType, IEnumerator loadCoroutine = null, Action loadComplete = null)
        {
            StartCoroutine(WaitForLoad());


            IEnumerator WaitForLoad()
            {
                // ���� ������� �ʱ�ȭ
                loadProgress = 0;

                // �ε� ���� ������� �۾��ϱ� ���ؼ� �ε� ���� ȣ��
                yield return SceneManager.LoadSceneAsync(SceneType.Loading.ToString());

                // ���ϴ� ���� �ҷ��� �ڿ� ��Ȱ��ȭ
                var asyncOper = SceneManager.LoadSceneAsync(sceneType.ToString(), LoadSceneMode.Additive);
                asyncOper.allowSceneActivation = false;

                if (loadCoroutine != null)
                {
                    // ���� �غ��ϴµ� �ʿ��� �۾��� �ִٸ� 
                    // �ش� �۾��� �Ϸ�� �� ���� ���
                    yield return StartCoroutine(loadCoroutine);
                }

                // �� ȣ���� ���� �� ���� �ݺ�
                while (!asyncOper.isDone)
                {
                    if (loadProgress >= .9f)
                    {
                        // �� ȣ���� ������ ������ �۾�
                        loadProgress = 1f;

                        yield return new WaitForSeconds(1f);

                        asyncOper.allowSceneActivation = true;
                    }
                    else
                    {
                        // �� �ε��� �����Ȳ�� ǥ��
                        loadProgress = asyncOper.progress;
                    }

                    // �ڷ�ƾ ������ �ݺ��� ��� ��
                    // ������ �� �� ���� �� ���� ������ ���� �� �� �ֵ���
                    yield return null;
                }

                // ���� ���� �غ� �ϱ� ���� �۾��� ��� �������Ƿ�, �ε��� ��Ȱ��ȭ
                yield return SceneManager.UnloadSceneAsync(SceneType.Loading.ToString());

                // �غ� �۾��� ������ �� �ڿ� ���� ��ų �۾��� �ִٸ� ����
                loadComplete?.Invoke();
            }

        }

    }
}