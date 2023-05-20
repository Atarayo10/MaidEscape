using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

namespace MaidEscape.UI
{
    /// <summary>
    /// ���� ������ �ȵ��
    /// </summary>
    public class UIBook : MonoBehaviour
    {
        // Public
        public GameObject bookGb;       // �� ������Ʈ
        public Button nextBtn;          // �� �ѱ�� ��ư

        // Private
        private Book book;              // Book ��ũ��Ʈ
        private int lastBookIndex;      // ������ ������ �ε���

        private void Start()
        {
            book = bookGb.GetComponent<Book>();

            // ������ ���� ���ϱ�
            lastBookIndex = book.bookPages.Count();
        }



    }
}