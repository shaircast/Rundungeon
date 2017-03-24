using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Living
{
    public GameObject stampCheck;

    public GameObject weapon;
    public GameObject armor;
    public GameObject accessory1;
    public GameObject accessory2;

    public List<GameObject> goodBuff; // buff is child GameObject.
    public List<GameObject> badBuff; // buff is child GameObject.

    // atk, atkCd, armor -> item.
    public int level;
    public int strength; // need for better equipment.
    private int xp;
    private int levelUpXp;
    public int maxJumpCount;
    private int currentJumpCount;
    public LayerMask groundTile;
    public int hunger; // 100 -> start losing hp.


    void Start()
    {
        currentJumpCount = maxJumpCount;
    }

    new void Update()
    {
        base.Update();
        CheckControlAndMove();
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
            if (Input.GetKeyDown(KeyCode.Space) && currentJumpCount > 0)
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
        if (Physics2D.OverlapCircle(stampCheck.transform.position, 0.1f, groundTile))
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

}
