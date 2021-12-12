using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Mushroom.cs
 * Made by YeongjinLim 101256371
 * Last modified in 2021-12-12
    Script for Mushroom Monster
 */
public class Mushroom : Monster
{
    public Transform[] wallCheck;

    private void Awake()
    {
        base.Awake();
        moveSpeed = 2f;
        jumpPower = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHit)
        {
            rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, rb.velocity.y);

            if (!Physics2D.OverlapCircle(wallCheck[0].position, 0.01f, layerMask) &&
                Physics2D.OverlapCircle(wallCheck[1].position, 0.01f, layerMask) &&
                 !Physics2D.Raycast(transform.position, -transform.localScale.x * transform.right, 1f, layerMask))
            {
                //Debug.Log("Hit");
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            else if (Physics2D.OverlapCircle(wallCheck[1].position, 0.01f, layerMask))
            {
                MonsterFlip();
            }
        }

    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.transform.CompareTag("PlayerHitBox"))
        {
            MonsterFlip();
        }
       
    }
}