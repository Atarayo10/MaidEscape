using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    MonsterControl mc;
    // Start is called before the first frame update
    void Start()
    {
        mc = GetComponentInParent<MonsterControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        else
        {
            mc.Chase(collision.gameObject);
        }
    }

}
