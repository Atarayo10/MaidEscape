using MaidEscape.Define;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaidEscape
{
    public class BookManager : MonoBehaviour
    {
        // Public 
        [Tooltip("���� ������ �Ѿ ��ư Ȱ��ȭ â")]
        public GameObject lastPanel;

        // Private
        private Book book;
        private int pageCount;       // ������ ����

        private void Start()
        {
            book = GetComponent<Book>();

            // ��ü ������ ������ ����
            pageCount = book.bookPages.Count();
        }

        private void Update()
        {
            if (book.currentPage == pageCount && 
                lastPanel.activeSelf == false)
            {
                // ���� �������� ������ ���������
                // ���� �ѱ� �� �ִ� â�� Ȱ��ȭ
                lastPanel.SetActive(true);
            }
        }

        /// <summary>
        /// �� ��ȯ ��ư ���ε� �޼���
        /// </summary>
        public void NextScene()
        {
            /// ������ �� Ÿ�Ը� �������� ���߿� �������� �Ŵ��� ����� �ϸ鼭
            /// IEnumerator�̶� Action ������ ÷�ν�ų����
            GameManager.Instance.LoadScene(SceneType.InGame);
        }
    }
}