using MaidEscape.Define;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace MaidEscape
    {
    public class Portal : MonoBehaviour
    {
        UIManager uiManager;
        CameraControl camControl;

        public enum MapNum
        {
            Forest,
            Tonw,
            Cave
        }
        MapNum map;
        private void Awake()
        {
            DontDestroyOnLoad(this.transform.parent);
            uiManager = FindObjectOfType<UIManager>();
            camControl = FindObjectOfType<CameraControl>();
            map = MapNum.Forest;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (this.map == MapNum.Forest)
                {
                    GameManager.Instance.LoadScene(SceneType.Map1_1);
                    StartCoroutine(LoadPlayer(collision.gameObject));
                    camControl.changeLimit(140, 40);
                }
            }
        }
        IEnumerator LoadPlayer(GameObject Player)
        {
            yield return new WaitForSeconds(1.5f);
            string name = "힐베리온의 숲" + System.Environment.NewLine;
            uiManager.GetComponent<UIManager>().ChangeStageLogo(name + this.transform.parent.name);
            Player.gameObject.transform.position = new Vector3(-5, 3, 0);
        }

    }
}
