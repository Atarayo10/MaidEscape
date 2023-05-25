using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    Collider2D coll;
    Rigidbody2D rg;
    SpriteRenderer spriteRenderer;
    GameObject lobbyFloor;

    float Horizontal;
    float Vertical;
    private float maxSpeed = 5f;
    private float jumpPower = 12f;
    private float maxSlopeAngle = 50;
    private const float RAY_DISTANCE = 1.2f;

    public float angle;
    private Vector2 perp;
    public GameObject checkPos;

    int playerLayer, groundLayer, ground2FLayer;

    [SerializeField] bool isSlope;
    [SerializeField] bool isJump;
    [SerializeField] bool isGround;
    [SerializeField] bool isGround2F;
    [SerializeField] bool isDownJump;

    //private int groundlayer;

    // Start is called before the first frame update
    void Start()
    {
        rg = this.GetComponent<Rigidbody2D>();
        coll = checkPos.GetComponent<Collider2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("Ground");
        ground2FLayer = LayerMask.NameToLayer("Ground2F");
    }

    // Update is called once per frame
    void Update()
    {
        Slope();
        Flip();
        GroundCheck();
    }

    private void FixedUpdate()
    {
        Jump();
        Move();
    }

    //캐릭터 좌우 변환
    void Flip()
    {
        //스프라이트 좌우 변경
        if (Horizontal > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Horizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    //경사로 체킹
    void Slope()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, RAY_DISTANCE, LayerMask.GetMask("Ground") | LayerMask.GetMask("Ground2F"));
        angle = Vector2.Angle(hit.normal, Vector2.up);
        perp = Vector2.Perpendicular(hit.normal).normalized;

        //Debug.Log(perp);

        if (angle != 0)
        {
            isSlope = true;
        }
        else
        {
            isSlope = false;
        }


        Debug.DrawLine(hit.point, hit.point + perp, Color.green); ;
        Debug.DrawLine(hit.point, hit.point + hit.normal, Color.blue);
    }

    void GroundCheck()
    {
        //발 밑 원형만들어서 땅 접촉 확인
        isGround = Physics2D.OverlapCircle(checkPos.transform.position, 0.2f, LayerMask.GetMask("Ground") | LayerMask.GetMask("Ground2F"));
        isGround2F = Physics2D.OverlapCircle(checkPos.transform.position, 0.2f, LayerMask.GetMask("Ground2F"));

        if (!isGround)
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }
    }

    void Jump()
    {
        //플레이어 점프
        if (Input.GetButtonDown("Jump") && isGround && !isJump)
        {
            isJump = true;
            rg.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        //아래 점프
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S) && isGround2F)
        {
            Debug.Log("downJump");
            isDownJump = true;
            StartCoroutine(downJump());
            rg.AddForce(Vector2.down * (jumpPower * 1.5f), ForceMode2D.Impulse);
        }
        if (rg.velocity.y > 0)
            Physics2D.IgnoreLayerCollision(playerLayer, groundLayer | ground2FLayer, true);
        else if (!isDownJump)
            Physics2D.IgnoreLayerCollision(playerLayer, groundLayer | ground2FLayer, false);
        //점프 시 레이어 무시
        //Debug.Log(rg.velocity.y);
    }


    IEnumerator downJump()
    {

        Physics2D.IgnoreLayerCollision(playerLayer, ground2FLayer, true);
        yield return new WaitForSeconds(1f);
        isDownJump = false;
        Physics2D.IgnoreLayerCollision(playerLayer, ground2FLayer, false);
    }


    // 플레이어 이동
    void Move()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        #region MOVE
        if (Horizontal != 0)
        {
            //경사로 이동
            if (isSlope && isGround && !isJump && angle < maxSlopeAngle)
            {
                //Vector2 t = new Vector2(perp.x * maxSpeed * Horizontal * Time.deltaTime,
                               // perp.y * maxSpeed * -Horizontal * Time.deltaTime);
                rg.velocity = Vector2.zero;
                if (Horizontal > 0)
                {
                    transform.Translate(new Vector2(perp.x * maxSpeed * -Horizontal * Time.deltaTime,
                        perp.y * maxSpeed * -Horizontal * Time.deltaTime));
                    //transform.Translate(-t);
                }
                else if (Horizontal < 0)
                {
                    //transform.Translate(t);
                    transform.Translate(new Vector2(perp.x * maxSpeed * Horizontal * Time.deltaTime,
                        perp.y * maxSpeed * -Horizontal * Time.deltaTime));
                }
            }
            //땅 이동
            else if (!isSlope && isGround && !isJump)
            {
                transform.Translate(Vector2.right * maxSpeed * Time.deltaTime * Mathf.Abs(Horizontal));
            }
            //예외 처리
            else if (!isGround)
            {
                transform.Translate(Vector2.right * maxSpeed * Time.deltaTime * Mathf.Abs(Horizontal));
            }
        }
        // 버튼 떼면 속도 정지로 만들기
        if (Input.GetButtonUp("Horizontal"))
        {
            rg.velocity = new Vector2(rg.velocity.normalized.x * 0.5f, rg.velocity.y);
        }
        if (Horizontal == 0)
            rg.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else
            rg.constraints = RigidbodyConstraints2D.FreezeRotation;
        #endregion
        //경사로
        #region MAXSPEED
        //오른쪽 최대 속력 제한
        if (rg.velocity.x > maxSpeed)
            rg.velocity = new Vector2(maxSpeed, rg.velocity.y);
        //왼쪽 최대 속력 제한
        else if (rg.velocity.x < maxSpeed * (-1))
        {
            rg.velocity = new Vector2(maxSpeed * (-1), rg.velocity.y);
        }
        #endregion

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "weapon")
        {

        }
    }


}
