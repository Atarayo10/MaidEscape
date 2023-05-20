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
    /// 로딩 씬을 컨트롤 하는 클래스
    /// </summary>
    public class UILoading : MonoBehaviour
    {
        // public
        public TextMeshProUGUI loadStateDesc;   // 로딩중입니다
        public Image loadGauge;                 // 로딩게이지
        public Image cutScene;                  // 로딩 컷씬


        // private
        private static string dot = string.Empty;
        private static string loadStateDescription = "로딩중 입니다";

        private void Start()
        {
            // 나중에 컷씬들 모아서 아틀라스로 만든다음 로딩씬 시작할때 컷신 설정하는 작업

        }

        private void Update()
        {
            if (loadGauge.fillAmount >= .88f)
            {
                // 로딩 완료
                loadGauge.fillAmount = 1f;
            }

            // Mathf.Lerp를 이용해서 값을 추출
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