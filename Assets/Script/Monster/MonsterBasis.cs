using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBasis : Living
{
    public enum monsterTier
    {
        Normal, Trained, Elite, Hero
    }
    // for ref.
    public GameObject target;

    // basic stats.
    public monsterTier tier;
    public int armor;
    public int atk;
    public float atkCooldown; // cooldown.
    public float atkPushbackPow; // pushing back.
    public int grantXp; // give to player when dies.
    public GameObject item; // drop it when dies.
    public List<GameObject> buff; // good&bad -> monsters doesn't purify themselves.

    public float alertRadius; // greater than sight Radius.
    public float atkRange; // if melee -> 0.
    public float sightRadius;

    // status
    public bool chasing;
    public bool alert;
    private bool playerGetsInAlertRadius; // one-time bool.

    new protected virtual void Awake()
    {
		base.Awake();
        StartCoroutine(CheckDistanceByTime()); // for being alert.
        playerGetsInAlertRadius = false;
    }


    // Update -> moving should be defined in each monster.
    new protected virtual void Update()
    {
        base.Update();

        // player check.
        if (GameObject.FindWithTag("Player") != null)
        {
            target = GameObject.FindWithTag("Player");
        }
        else
        {

        }

        // monster moves when they're alert(not sleepeing).
        if (!alert)
        {
            cannotDo = true;
            cannotMove = true;
            // check distance with player and set alert.
            if (playerGetsInAlertRadius)
            {
                MonsterAwakeByPlayer();
            }
        }
    }


    IEnumerator CheckDistanceByTime() // Mosnter checks distance with player in routine.
    {
        while (true)
        {
			Debug.Log("Check distance");
            if (target != null) // only check there's player.
            {
                // check alert.
                if (Vector2.Distance(transform.position, target.transform.position) <= alertRadius)
                {
                    playerGetsInAlertRadius = true;
                }
                else
                {
                    playerGetsInAlertRadius = false;
                }

                // check chasing.
                if (Vector2.Distance(transform.position, target.transform.position) <= sightRadius)
                {
                    chasing = true;
                }
                else
                {
                    chasing = false;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }



    void MonsterAwakeByPlayer() // alert and chase.
    {
        Debug.Log("Awake");
        alert = true;
        chasing = true;
        cannotDo = false;
        cannotMove = false;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") // battle.
        {
            PushedMeback(other.gameObject, (PlayerController.singleton.weapon == null)? 0f : PlayerController.singleton.weapon.GetComponent<Weapon>().pushbackPower); // 30f -> player's weapon's property.
        }
    }

    void chasePlayerBasic()
    {

    }


}