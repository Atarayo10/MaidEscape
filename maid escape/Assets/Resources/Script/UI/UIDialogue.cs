using MaidEscape.UIElement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidEscape.UI
{
    public class UIDialogue : MonoBehaviour
    {
        [SerializeField]
        private GameObject dialogueBox;

        /// <summary>
        /// 다이얼로그 박스를 활성화 하는 메서드
        /// </summary>
        /// <param name="isInteraction"> 상호작용 여부 </param>
        public void OnDialogueBox(bool isInteraction)
        {
            if (dialogueBox != null && isInteraction && !dialogueBox.activeSelf)
            {
                dialogueBox.SetActive(isInteraction);
            }
        }

        /// <summary>
        /// 다이얼로그 박스를 비활성화 하는 메서드
        /// </summary>
        /// <param name="isInteraction"> 상호작용 여부 </param>
        public void OffDialogueBox(bool isInteraction)
        {
            if (dialogueBox != null && !isInteraction && dialogueBox.activeSelf)
            {
                dialogueBox.SetActive(isInteraction);
            }
        }
    }
}