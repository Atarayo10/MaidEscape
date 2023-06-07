using MaidEscape.Define;
using MaidEscape.UI;
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
    [SerializeField] private float maxSpeed = 3f;
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
    [SerializeField] bool isInteraction;
    [SerializeField] bool isroot;

    [SerializeField] UIDialogue uiDialogue;

    PlayerStat playerStat = new PlayerStat(100, 10.0f, 10.0f, 1.0f, 10.0f, 20.0f, 10.0f);
    PlayerInven playerInven;

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


    void Start()
    {
        rg = this.GetComponent<Rigidbody2D>();
        coll = checkPos.GetComponent<Collider2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        playerInven = GameObject.FindObjectOfType<PlayerInven>();

        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("Ground");
        ground2FLayer = LayerMask.NameToLayer("Ground2F");
    }


    void Update()
    {
        Jump();
        Slope();
        Flip();
        GroundCheck();
        CheckInteraction();
    }

    private void FixedUpdate()
    {
        //Jump();
        Move();
    }

    //筌?Ŧ????ル슣??癰궰??
    void Flip()
    {
        //??쎈늄??깆뵠???ル슣??癰궰野?
        if (Horizontal > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Horizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    //野껋럩沅쀦에?筌ｋ똾沅?
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
        //獄?獄??癒곗굨筌띾슢諭??곴퐣 ???臾믫겫 ?類ㅼ뵥
        isGround = Physics2D.OverlapCircle(checkPos.transform.position, 0.3f, LayerMask.GetMask("Ground") | LayerMask.GetMask("Ground2F"));
        isGround2F = Physics2D.OverlapCircle(checkPos.transform.position, 0.3f, LayerMask.GetMask("Ground2F"));

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
        //???쟿??곷선 ?癒곕늄
        if (Input.GetKeyDown(KeyCode.Space) && isGround && !isJump)
        {
            isJump = true;
            rg.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        #endregion
        #region DOWNJUMP
        //?袁⑥삋 ?癒곕늄
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S) && isGround2F)
        {
            Debug.Log("downJump");
            isDownJump = true;
            StartCoroutine(downJump());
            rg.AddForce(Vector2.down * (jumpPower * 1.5f), ForceMode2D.Impulse);
        }
        #endregion
        #region FLOORJUMP
        //?癒곕늄 ????됱뵠???얜똻??
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


    // ???쟿??곷선 ??猷?
    void Move()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        #region MOVE
        if (Horizontal != 0)
        {
            //野껋럩沅쀦에???猷?
            if (isSlope && isGround && !isJump && angle < maxSlopeAngle)
            {
                rg.velocity = Vector2.zero;
                if (Horizontal > 0 && !isJump)
                {
                    transform.Translate(new Vector2(perp.x * maxSpeed * -Horizontal * Time.deltaTime,
                        perp.y * maxSpeed * -Horizontal * Time.deltaTime));
                }
                else if (Horizontal < 0 && !isJump)
                {
                    transform.Translate(new Vector2(perp.x * maxSpeed * Horizontal * Time.deltaTime,
                        perp.y * maxSpeed * -Horizontal * Time.deltaTime));
                }
            }
            //????猷?
            else if (!isSlope && isGround && !isJump)
            {
                transform.Translate(Vector2.right * maxSpeed * Time.deltaTime * Mathf.Abs(Horizontal));
            }
            //??됱뇚 筌ｌ꼶??
            else if (!isGround)
            {
                transform.Translate(Vector2.right * maxSpeed * Time.deltaTime * Mathf.Abs(Horizontal));
            }
        }
        // 甕곌쑵????겹늺 ??얜즲 ?類?嚥?筌띾슢諭얏묾?
        if (Input.GetButtonUp("Horizontal"))
        {
            rg.velocity = new Vector2(rg.velocity.normalized.x * 0.5f, rg.velocity.y);
        }
        if (Horizontal == 0)
            rg.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else
            rg.constraints = RigidbodyConstraints2D.FreezeRotation;
        #endregion
        //野껋럩沅쀦에?
        #region MAXSPEED
        //??삘뀲筌?筌ㅼ뮆? ??얠젾 ??쀫립
        if (rg.velocity.x > maxSpeed)
            rg.velocity = new Vector2(maxSpeed, rg.velocity.y);
        //??긱걹 筌ㅼ뮆? ??얠젾 ??쀫립
        else if (rg.velocity.x < maxSpeed * (-1))
        {
            rg.velocity = new Vector2(maxSpeed * (-1), rg.velocity.y);
        }
        #endregion

    }

    /// <summary>
    /// 상호작용을 항시 체크하는 메서드
    /// </summary>
    private void CheckInteraction()
    {
        // 만약 플레이어 주변에 NPC가 존재한다면
        if (isInteraction && Input.GetButtonDown("Interaction"))
        {
            uiDialogue.OnDialogueBox(isInteraction);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 만약 플레이어가 Npc에 닿았다면
        if (collision.gameObject.tag == ObjectType.Npc.ToString())
        {
            // 상호작용 시작가능
            isInteraction = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 만약 플레이어가 Npc에서 떨어진다면
        if (collision.gameObject.tag == ObjectType.Npc.ToString())
        {
            // 상호작용 종료
            isInteraction = false;

            uiDialogue.OffDialogueBox(isInteraction);
        }
    }

    void pick(Collider2D collision)
    {
        Debug.Log("Trigger enter");
        playerInven.PickItem(collision.GetComponent<Item>().getitemId(), collision.gameObject);
        isroot = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            isroot = true;
            pick(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isroot == true && Input.GetKeyDown(KeyCode.X))
        {
            pick(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            isroot = false;
        }
    }


}
