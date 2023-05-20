using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

namespace MaidEscape.UI
{
    /// <summary>
    /// 뭔가 마음에 안들어
    /// </summary>
    public class UIBook : MonoBehaviour
    {
        // Public
        public GameObject bookGb;       // 북 오브젝트
        public Button nextBtn;          // 씬 넘기는 버튼

        // Private
        private Book book;              // Book 스크립트
        private int lastBookIndex;      // 마지막 페이지 인덱스

        private void Start()
        {
            book = bookGb.GetComponent<Book>();

            // 페이지 개수 구하기
            lastBookIndex = book.bookPages.Count();
        }



    }
}