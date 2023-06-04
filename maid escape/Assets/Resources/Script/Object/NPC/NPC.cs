using MaidEscape.UIElement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidEscape.Object
{
    public class NPC : MonoBehaviour
    {
        [SerializeField]

        private void Awake()
        {
            ChatBox chatBox = GetComponent<ChatBox>();
            chatBox.Initialize(this.gameObject.GetComponent<NPC>());
        }

        public void Initialize()
        {

        }

        public void NpcUpdate()
        {

        }
    }
}