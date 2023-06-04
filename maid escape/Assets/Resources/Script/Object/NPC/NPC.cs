using MaidEscape.UIElement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidEscape.Object
{
    public class NPC : MonoBehaviour
    {
        [SerializeField]
        private ChatBox chatBox;        // ChatBox 클래스

        public Collider2D Coll { get; set; }       // 현재 NPC가 지닌 콜라이더의 프로퍼티

        private void Awake()
        {
            chatBox.Initialize(this);
            Coll = GetComponent<Collider2D>();
        }
    }
}