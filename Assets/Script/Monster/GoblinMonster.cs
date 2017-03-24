using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMonster : MonsterBasis
{
    void Start()
    {

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        // goblin's specific moving style.

        // moves only when alert.
        if (alert)
        {
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
            }
            else // cannotMove is true;
            {
                dampMoveExceptFalling();
            }
        }
        else // sleeping.
        {
            dampMoveExceptFalling();
        }

    }

}
