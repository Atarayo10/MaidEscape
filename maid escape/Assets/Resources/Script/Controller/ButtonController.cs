using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace MaidEscape
{
    /// <summary>
    /// 현재 씬에 존재하는 버튼들을 관리하는 클래스
    /// </summary>
    public class ButtonController : MonoBehaviour
    {
        // Public
        public GameObject BtnWin;       // 버튼 창
        public GameObject OpWin;        // 옵션 창

        // Private

        /// <summary>
        /// 현재 씬에 존재하는 버튼을 관리하는 메서드
        /// </summary>
        public void ButtonOption()
        {
            // 창에 있는 버튼들의 순서를 체크
            switch (EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex())
            {
                // 새로하기
                case 0:
                    SceneManager.LoadScene("FirstLoading");
                    break;

                // 이어하기
                case 1:
                    SceneManager.LoadScene("FirstLoading");
                    break;

                // 옵션
                case 2:
                    // 현재 버튼 홀더가 꺼지고
                    // 옵션 창이 활성화 되야한다
                    BtnWin.SetActive(false);
                    OpWin.SetActive(true);
                    break;

                // 나가기
                case 3:
                    break;
            }
        }

        /// <summary>
        /// 옵션 창 Exit 버튼에 바인딩할 메서드
        /// </summary>
        public void OpBtnBinding()
        {
            BtnWin.SetActive(true);
            OpWin.SetActive(false);
        }
    }
}