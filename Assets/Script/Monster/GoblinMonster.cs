using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMonster : MonsterBasis
{
    new void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        // goblin's specific moving style.


        if (alert) // alert
        {
            if (cannotMove != true) // alert and can move.
            {
                if (chasing) // chasing AI.
                {
                    // horizontal
                    if (target.transform.position.x < transform.position.x) // player is left side of monster.
                    {
                        goRight = false;
                    }
                    else // player is right side of monster.
                    {
                        goRight = true;
                    }
                }

                BasicMove(); // regardless chasing, do basic move.
            }
            else // alert but cannot move.
            {
                dampMoveExceptFalling();
            }
        }
        else // not alert == sleeping.
        {
            dampMoveExceptFalling();
        }

    }

    void BasicMove()
    {
        if (goRight)
        {
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
        }
    }

}
