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
    [SerializeField]private float maxSpeed = 3f;
    private float jumpPower = 14f;
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

    PlayerStat playerStat = new PlayerStat(100, 10.0f, 10.0f, 1.0f, 10.0f, 20.0f, 5.0f);

    //private int groundlayer;

    void SetUP()
    {
        maxSpeed = playerStat.Speed;
        DontDestroyOnLoad(this.gameObject);
    }

    public PlayerStat returnStat()
    {
        return playerStat;
    }

    private void Awake()
    {
        SetUP();
    }
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
        Jump();
        Slope();
        Flip();
        GroundCheck();
    }

    private void FixedUpdate()
    {
        //Jump();
        Move();
    }

    //罹먮┃??醫뚯슦 蹂??
    void Flip()
    {
        //?ㅽ봽?쇱씠??醫뚯슦 蹂寃?
        if (Horizontal > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Horizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    //寃쎌궗濡?泥댄궧
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
        //諛?諛??먰삎留뚮뱾?댁꽌 ???묒큺 ?뺤씤
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
        #region JUMP
        //?뚮젅?댁뼱 ?먰봽
        if (Input.GetKeyDown(KeyCode.Space) && isGround && !isJump)
        {
            isJump = true;
            rg.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        #endregion
        #region DOWNJUMP
        //?꾨옒 ?먰봽
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S) && isGround2F)
        {
            Debug.Log("downJump");
            isDownJump = true;
            StartCoroutine(downJump());
            rg.AddForce(Vector2.down * (jumpPower * 1.5f), ForceMode2D.Impulse);
        }
        #endregion
        #region FLOORJUMP
        //?먰봽 ???덉씠??臾댁떆
        //Debug.Log(rg.velocity.y);
        if (rg.velocity.y > 0)
            Physics2D.IgnoreLayerCollision(playerLayer, groundLayer | ground2FLayer, true);
        else if (!isDownJump)
            Physics2D.IgnoreLayerCollision(playerLayer, groundLayer | ground2FLayer, false);
        #endregion
    }


    IEnumerator downJump()
    {

        Physics2D.IgnoreLayerCollision(playerLayer, ground2FLayer, true);
        yield return new WaitForSeconds(1f);
        isDownJump = false;
        Physics2D.IgnoreLayerCollision(playerLayer, ground2FLayer, false);
    }


    // ?뚮젅?댁뼱 ?대룞
    void Move()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        #region MOVE
        if (Horizontal != 0)
        {
            //寃쎌궗濡??대룞
            if (isSlope && isGround && !isJump && angle < maxSlopeAngle)
            {
                rg.velocity = Vector2.zero;
                if (Horizontal > 0)
                {
                    transform.Translate(new Vector2(perp.x * maxSpeed * -Horizontal * Time.deltaTime,
                        perp.y * maxSpeed * -Horizontal * Time.deltaTime));
                }
                else if (Horizontal < 0)
                {
                    transform.Translate(new Vector2(perp.x * maxSpeed * Horizontal * Time.deltaTime,
                        perp.y * maxSpeed * -Horizontal * Time.deltaTime));
                }
            }
            //???대룞
            else if (!isSlope && isGround && !isJump)
            {
                transform.Translate(Vector2.right * maxSpeed * Time.deltaTime * Mathf.Abs(Horizontal));
            }
            //?덉쇅 泥섎━
            else if (!isGround)
            {
                transform.Translate(Vector2.right * maxSpeed * Time.deltaTime * Mathf.Abs(Horizontal));
            }
        }
        // 踰꾪듉 ?쇰㈃ ?띾룄 ?뺤?濡?留뚮뱾湲?
        if (Input.GetButtonUp("Horizontal"))
        {
            rg.velocity = new Vector2(rg.velocity.normalized.x * 0.5f, rg.velocity.y);
        }
        if (Horizontal == 0)
            rg.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else
            rg.constraints = RigidbodyConstraints2D.FreezeRotation;
        #endregion
        //寃쎌궗濡?
        #region MAXSPEED
        //?ㅻⅨ履?理쒕? ?띾젰 ?쒗븳
        if (rg.velocity.x > maxSpeed)
            rg.velocity = new Vector2(maxSpeed, rg.velocity.y);
        //?쇱そ 理쒕? ?띾젰 ?쒗븳
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
