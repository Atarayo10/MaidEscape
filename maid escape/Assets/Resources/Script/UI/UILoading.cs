using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.UI;

namespace MaidEscape.UI
{
    /// <summary>
    /// �ε� ���� ��Ʈ�� �ϴ� Ŭ����
    /// </summary>
    public class UILoading : MonoBehaviour
    {
        // public
        public TextMeshProUGUI loadStateDesc;   // �ε����Դϴ�
        public Image loadGauge;                 // �ε�������
        public Image cutScene;                  // �ε� �ƾ�


        // private
        private static string dot = string.Empty;
        private static string loadStateDescription = "�ε��� �Դϴ�";

        private void Start()
        {
            // ���߿� �ƾ��� ��Ƽ� ��Ʋ�󽺷� ������� �ε��� �����Ҷ� �ƽ� �����ϴ� �۾�

        }

        private void Update()
        {
            if (loadGauge.fillAmount >= .88f)
            {
                // �ε� �Ϸ�
                loadGauge.fillAmount = 1f;
            }

            // Mathf.Lerp�� �̿��ؼ� ���� ����
            loadGauge.fillAmount = Mathf.Lerp(loadGauge.fillAmount, 1f, Time.deltaTime * 2f);

            if (Time.frameCount % 20 == 0)
            {
                if (dot.Length >= 3)
                {
                    dot = string.Empty;
                }
                else
                {
                    dot = string.Concat(dot, ".");
                }

                loadStateDesc.text = $"{loadStateDescription} {dot}";
            }
        }
    }
}