using MaidEscape.Object;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MaidEscape.UIElement
{
    public class ChatBox : MonoBehaviour
    {
        [SerializeField]
        private float width;

        [SerializeField]
        private float height;

        private NPC target;     // ChatBox가 따라다닐 NPC
        private RectTransform rect;

        private void Update()
        {
           Vector3 chatBoxPos =     Camera.main.WorldToScreenPoint(new Vector3(target.transform.position.x + width,
               target.transform.position.y + height, 0));

            this.transform.position = chatBoxPos;
        }

        /// <summary>
        /// ChatBox의 초기 세팅을 담당하는 메서드
        /// </summary>
        public void Initialize(NPC target)
        {
            this.target = target;
            transform.localScale = Vector2.one;
            rect = GetComponent<RectTransform>();
        }
    }
}