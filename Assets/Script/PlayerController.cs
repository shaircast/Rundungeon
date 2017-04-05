using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Living
{
    public GameObject stampCheck;
    public static PlayerController singleton;
    Camera mainCam;
    Vector3 camPosition;
    public float camTargetPositionY;

    public int inventorySize;
    public List<GameObject> inventory;
    public GameObject[] quickSlot;
    public GameObject weapon;
    public GameObject armor;
    public GameObject accessory1;
    public GameObject accessory2;

    public GameObject uiInventory;
    public GameObject uiQuickslot;

    public List<GameObject> goodBuff; // buff is child GameObject.
    public List<GameObject> badBuff; // buff is child GameObject.

    // atk, atkCd, armor -> item.
    public int level;
    public int strength; // need for better equipment.
    private int xp;
    private int levelUpXp;
    public int maxJumpCount;
    private int currentJumpCount;
    public int hunger; // 100 -> start losing hp.
    public bool isOnGround;

    new void Awake()
    {
        base.Awake();
        singleton = this;
        Debug.Log("player awake");
        // cam initialize.
        mainCam = Camera.main;
        camTargetPositionY = 3.3f;
        camPosition = new Vector3(transform.position.x, camTargetPositionY, -10f);

    }

    void Start()
    {
        currentJumpCount = maxJumpCount;
        StartCoroutine(SmoothCamCoroutine());
    }

    new void Update()
    {
        base.Update();
        isOnGround = Physics2D.OverlapCircle(stampCheck.transform.position, 0.2f, 1 << LayerMask.NameToLayer("Tile")) != null
                    || Physics2D.OverlapCircle(stampCheck.transform.position, 0.2f, 1 << LayerMask.NameToLayer("Wall")) != null;
        CheckControlAndMove(); // make player move.
        
        // set cam position
        SetCamPositionY();
        camPosition.x = transform.position.x;
        mainCam.transform.position = camPosition;
    }

    void SetCamPositionY()
    {
        float radius = 3.3f;
        // center of ceiling and floor.
        RaycastHit2D upward = Physics2D.Raycast(gameObject.transform.position, Vector3.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("Tile"));
        RaycastHit2D downward = Physics2D.Raycast(gameObject.transform.position, -Vector3.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("Tile"));

        // but not too far from player. +-2f.
        camTargetPositionY =  Mathf.Clamp((upward.transform.position.y + downward.transform.position.y)/2, transform.position.y - radius, transform.position.y + radius);
    }

    IEnumerator SmoothCamCoroutine()
    {
        float smoothFactor = 20f; // higher = slower&smoother.
        while (true)
        {
            if (camTargetPositionY > camPosition.y) 
            {
                camPosition.y += (camTargetPositionY - camPosition.y) /smoothFactor;
            }
            else
            {   
                camPosition.y -= (camPosition.y - camTargetPositionY) /smoothFactor;
            }
            yield return new WaitForEndOfFrame();
        }
    }


    void CheckControlAndMove()
    {
        // about direction
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (goRight)
                goRight = false;
            else
                goRight = true;
        }

        // about moving
        if (cannotMove != true)
        {
            // left & right
            if (goRight)
            {
                rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            }

            // jump
            if (Input.GetKeyDown(KeyCode.J) && currentJumpCount > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
                currentJumpCount--;
            }
        }
        else // cannotMove is true;
        {
            dampMoveExceptFalling();
        }

        // stamp ground charges jumpCount up.
        if (isOnGround)
        {
            currentJumpCount = maxJumpCount - 1;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Monster") // battle.
        {
            PushedMeback(other.gameObject, other.gameObject.GetComponent<MonsterBasis>().atkPushbackPow);

        }
    }

    void CheckItemInInventory(int index) // show info fo the itme at index in inventory.
    {
        inventory[index].GetComponent<Item>().ShowInfo(index);
    }

    void UseItemInInventory(int index) // use item at index in inventory
    {
        inventory[index].GetComponent<Item>().UseItem(index);
    }

}