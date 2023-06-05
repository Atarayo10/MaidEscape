using MaidEscape.Define;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace MaidEscape
{
    /// <summary>1
    /// 현재 씬에 존재하는 버튼들을 관리하는 클래스
    /// </summary>
    public class ButtonController : MonoBehaviour
    {
        /// <summary>
        /// 현재 씬에 존재하는 버튼을 관리하는 메서드
        /// </summary>
        public void MainButton()
        {
            SceneManager.LoadScene(SceneType.FirstLoading.ToString());
        }
    }
}