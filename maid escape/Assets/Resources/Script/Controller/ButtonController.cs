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
        // Public
        public GameObject BtnWin;       // ��ư â
        public GameObject OpWin;        // �ɼ� â

        // Private

        /// <summary>
        /// ���� ���� �����ϴ� ��ư�� �����ϴ� �޼���
        /// </summary>
        public void ButtonOption()
        {
            // â�� �ִ� ��ư���� ������ üũ
            switch (EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex())
            {
                // �����ϱ�
                case 0:
                    SceneManager.LoadScene("FirstLoading");
                    break;

                // �̾��ϱ�
                case 1:
                    SceneManager.LoadScene("FirstLoading");
                    break;

                // �ɼ�
                case 2:
                    // ���� ��ư Ȧ���� ������
                    // �ɼ� â�� Ȱ��ȭ �Ǿ��Ѵ�
                    BtnWin.SetActive(false);
                    OpWin.SetActive(true);
                    break;

                // ������
                case 3:
                    break;
            }
        }

        /// <summary>
        /// �ɼ� â Exit ��ư�� ���ε��� �޼���
        /// </summary>
        public void OpBtnBinding()
        {
            BtnWin.SetActive(true);
            OpWin.SetActive(false);
        }
    }
}