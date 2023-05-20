using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    enum Type { weapon, coin, objects, dropItem, unknown }
    [SerializeField] Type type;
    [SerializeField] ParticleSystem particle;
    [SerializeField]SpriteAtlas spAtlas;
    SpriteRenderer spRender;

    Vector3 origintr;

    //move range
    float max, min;
    float movespeed = 1;

    //itemData
    int ranItem;
    [SerializeField] int itemid;
    [SerializeField]Sprite itemImage;
    [SerializeField] string itemInfo;
    [SerializeField] string spName;

    PlayerInven playerInven;

    private void Awake()
    {
        origintr = this.gameObject.transform.position;
        particle = this.gameObject.GetComponent<ParticleSystem>(); 
        itemImage = this.gameObject.GetComponent<Sprite>();
        spAtlas = Resources.Load<SpriteAtlas>("Prefabs/Weapon/Weapons");
        spRender = this.gameObject.GetComponent<SpriteRenderer>();
        playerInven = GameObject.FindObjectOfType<PlayerInven>();
        particle.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        max = origintr.y + 0.2f;
        min = origintr.y - 0.2f;

        #region typeInit
        if (this.gameObject.tag == "weapon")
        {
            type = Type.weapon;
            weaponInit();
        }
        else if(this.gameObject.tag == " coin")
        {
            type = Type.coin;
        }
        else if(this.gameObject.tag == "objects")
        {
            type = Type.objects;
        }
        else if(this.gameObject.tag == "dropItem")
        {
            type = Type.dropItem;
        }
        else
        {
            type = Type.unknown;
        }
        #endregion

    }

    void weaponInit()
    {
        DataManager.Instance().LoadDatas();
        ranItem = Random.Range(101, 104);
        particle.Play();

        var data = DataManager.Instance().dicItemData[ranItem];
        itemid = data.Id;
        itemInfo = data.Info;
        //spriteName Set
        spName = data.spriteName;
        //itemImage load ( using spriteName )
        itemImage = this.spAtlas.GetSprite(spName);
        spRender.sprite = itemImage;

    }

    void itemMove()
    {
        Vector3 tr = this.gameObject.transform.position;

        if (tr.y  < min)
        {
            movespeed = 0.5f;
            //Debug.Log(tr.y);
        }
        else if (tr.y > max)
        {
            movespeed = -0.5f;
            //Debug.Log (tr.y);
        }

        tr.y += movespeed * Time.deltaTime;
        transform.position = tr;
    }

    // Update is called once per frame
    void Update()
    {
         itemMove(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Trigger enter");
            playerInven.PickItem(itemid, this.gameObject);
            
        }
    }

}
