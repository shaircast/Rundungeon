﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Living : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public int maxHp;
    public int hp;
    public float moveSpeed;
    public float jumpHeight;
    public bool cannotMove;
    public bool cannotDo;
    public bool goRight;

    // battle
    public float stiffTime;
    public float dampRateWhenStiff;

    // Use this for initialization
    protected virtual void Awake()
    {
        // CSV
        rb2d = GetComponent<Rigidbody2D>();
        stiffTime = 0.6f;
        dampRateWhenStiff = 0.9f; // it would be good to match with damage.

        // initialize monster to go right by 50%.
        if (Random.Range(0, 2) == 1 ? true : false)
        {
            goRight = true;
        }
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        FlipCheck();

    }

    void FlipCheck() //  for animation. defalut is lefty.
    {
        if (!cannotMove) // only when not stiff.
        {
            if (goRight)
            {
                transform.localScale = new Vector3(-1f, 1f, 0);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 0);
            }
        }

    }

    public void dampVeloX() // damp vel when battle stiff.
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x * dampRateWhenStiff, rb2d.velocity.y);
    }
    public void dampVeloY() // damp vel when battle stiff.
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * dampRateWhenStiff);
    }

    public void dampMoveExceptFalling() // damp velocity by axis except downward movement.
    {
        if (rb2d.velocity != Vector2.zero)
        {
            dampVeloX();
            if (rb2d.velocity.y > 0)
                dampVeloY();
        }
    }

    public void PushedMeback(GameObject pushingObject, float pushForce)
    // pusingObject pushes this object by pushForce. always bounce upwards.
    {
        Vector2 forceVector = (transform.position - pushingObject.transform.position).normalized * pushForce;
        Vector2 forceVectorPositiveY = new Vector2 (forceVector.x, (forceVector.y > 0) ? forceVector.y : -forceVector.y);
        StartCoroutine(StartPushedStiffDuringTime(stiffTime));
        rb2d.AddForce(forceVectorPositiveY, ForceMode2D.Impulse);
        Debug.Log(forceVectorPositiveY);
        Debug.Log("pushed back");
    }

    IEnumerator StartPushedStiffDuringTime(float sec) // stiff for sec.
    {
        cannotDo = true;
        cannotMove = true;

        yield return new WaitForSeconds(sec);
        cannotDo = false;
        cannotMove = false;
    }

}
