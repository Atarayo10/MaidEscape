using MaidEscape.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidEscape.UIElement
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField]
        private float height;

        private PlayerControl target;             // 따라 다닐 타겟
        private RectTransform rect;

        private void Update()
        {
            Vector3 chatBoxPos = Camera.main.WorldToScreenPoint(new Vector3(target.transform.position.x,
                target.transform.position.y + height, 0));

            rect.position = chatBoxPos;
        }

        /// <summary>
        /// ChatBox의 초기 세팅을 담당하는 메서드
        /// </summary>
        public void Initialize(PlayerControl target)
        {
            this.target = target;
            transform.localScale = Vector2.one;
            rect = GetComponent<RectTransform>();
        }
    }
}