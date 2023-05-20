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
        public TextMeshProUGUI loadStateDesc;   // 로딩바 텍스트
        public Image loadFillGauge;             // 로딩바 이미지

        // Private
        private Image backImage;                // 컷 씬


        /// <summary>
        /// 현재 진행 상태를 텍스트에 표시해주는 메서드
        /// </summary>
        /// <param name="loadState"> 진행 상태 </param>
        public void SetLoadStateDescription(string loadState)
        {
            loadStateDesc.text = $"Load{loadState}";
        }

        /// <summary>
        /// 현재 진행 상태를 게이지에 표시해주는 코루틴
        /// </summary>
        /// <param name="loadPer"> 진행 퍼센트 </param>
        /// <returns></returns>
        public IEnumerator LoadGaugeUpdate(float loadPer)
        {
            // 두 값이 비슷 할 때 까지 반복
            while (!Mathf.Approximately(loadFillGauge.fillAmount, loadPer))
            {
                // 선형 보간으로 게이지를 세팅
                loadFillGauge.fillAmount = Mathf.Lerp(loadFillGauge.fillAmount, loadPer, Time.deltaTime * 2);
                yield return null;
            }
        }
    }
}