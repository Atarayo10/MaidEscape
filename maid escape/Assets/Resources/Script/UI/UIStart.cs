using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MaidEscape.UI
{
    public class UIStart : MonoBehaviour
    {
        // Public
        public TextMeshProUGUI loadStateDesc;   // �ε��� �ؽ�Ʈ
        public Image loadFillGauge;             // �ε��� �̹���

        // Private
        private Image backImage;                // �� ��


        /// <summary>
        /// ���� ���� ���¸� �ؽ�Ʈ�� ǥ�����ִ� �޼���
        /// </summary>
        /// <param name="loadState"> ���� ���� </param>
        public void SetLoadStateDescription(string loadState)
        {
            loadStateDesc.text = $"Load{loadState}";
        }

        /// <summary>
        /// ���� ���� ���¸� �������� ǥ�����ִ� �ڷ�ƾ
        /// </summary>
        /// <param name="loadPer"> ���� �ۼ�Ʈ </param>
        /// <returns></returns>
        public IEnumerator LoadGaugeUpdate(float loadPer)
        {
            // �� ���� ��� �� �� ���� �ݺ�
            while (!Mathf.Approximately(loadFillGauge.fillAmount, loadPer))
            {
                // ���� �������� �������� ����
                loadFillGauge.fillAmount = Mathf.Lerp(loadFillGauge.fillAmount, loadPer, Time.deltaTime * 2);
                yield return null;
            }
        }
    }
}