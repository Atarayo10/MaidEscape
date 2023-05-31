using MaidEscape.Define;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("portal : " + SceneType.Map.ToString() + this.transform.parent.name);
            SceneManager.LoadScene(SceneType.Map.ToString() + this.transform.parent.name);
        }
    }

}
