using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterControl : MonoBehaviour
{
    float moveSpeed = 1.5f;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    int ranTime, ranDir;
    int moveDir = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x > 0.1f)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX=false;
        }

        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir, rb.velocity.y);
    }

    public void Chase(GameObject target)
    {
        if (target.transform.position.x > transform.position.x)
        {
            moveDir = 3;
        }
        else if (target.transform.position.x < transform.position.x)
        {
            moveDir = -3;
        }
    }

}
