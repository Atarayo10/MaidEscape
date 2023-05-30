using MaidEscape.Define;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaidEscape
{
    public class BookManager : MonoBehaviour
    {
        // Public 
        [Tooltip("다음 씬으로 넘어갈 버튼 활성화 창")]
        public GameObject lastPanel;

        // Private
        private Book book;
        private int pageCount;       // 페이지 갯수

        private void Start()
        {
            book = GetComponent<Book>();

            // 전체 페이즈 갯수를 저장
            pageCount = book.bookPages.Count();
        }

        private void Update()
        {
            if (book.currentPage == pageCount && 
                lastPanel.activeSelf == false)
            {
                // 현재 페이지가 마지막 페이지라면
                // 씬을 넘길 수 있는 창을 활성화
                lastPanel.SetActive(true);
            }
        }

        /// <summary>
        /// 씬 전환 버튼 바인딩 메서드
        /// </summary>
        public void NextScene()
        {
            /// 지금은 씬 타입만 들어가있지만 나중에 스테이지 매니저 만들고 하면서
            /// IEnumerator이랑 Action 같은거 첨부시킬예정
            GameManager.Instance.LoadScene(SceneType.Lobby);
        }
    }
}