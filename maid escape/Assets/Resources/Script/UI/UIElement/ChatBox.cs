using MaidEscape.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidEscape.UIElement
{
    public class ChatBox : MonoBehaviour
    {
        /*
         * 1. ChatBox가 NPC를 따라 다녀야함
         * 2. 
         */

        private NPC target;     // ChatBox가 따라다닐 NPC

        /// <summary>
        /// ChatBox의 초기 세팅을 담당하는 메서드
        /// </summary>
        public void Initialize(NPC target)
        {
            this.target = target;
            transform.localScale = Vector3.one;
        }
    }
}