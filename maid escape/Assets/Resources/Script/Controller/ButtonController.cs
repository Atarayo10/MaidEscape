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
    /// <summary>
    /// ���� ���� �����ϴ� ��ư���� �����ϴ� Ŭ����
    /// </summary>
    public class ButtonController : MonoBehaviour
    {
        #region Public Variable

        #endregion 

        #region Private Variable
        
        [SerializeField]
        private GameObject OpWin;        // �ɼ� â

        #endregion

        /// <summary>
        /// ���� ���� �����ϴ� ��ư�� �����ϴ� �޼���
        /// </summary>
        public void MainButton()
        {
            SceneManager.LoadScene(SceneType.FirstLoading.ToString());
        }
    }
}