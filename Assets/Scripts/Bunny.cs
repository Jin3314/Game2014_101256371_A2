using System.Collections;
using UnityEngine;
/*
 * Bunny.cs
 * Made by YeongjinLim 101256371
 * Last modified in 2021-12-12
    Script for Bunny monster
 */
public class Bunny : Monster
{
    public enum State
    {
        Idle,
        Run,
        Attack,
        Jump
    };
    public State currentState = State.Idle;

    public Transform[] wallCheck;
    WaitForSeconds Delay500 = new WaitForSeconds(0.5f);

    Vector2 boxColliderOffset;
    Vector2 boxColliderJumpOffset;

    void Awake()
    {
        base.Awake();
        moveSpeed = 3f;
        jumpPower = 25f;
        currentHp = 6;
        atkCoolTime = 3f;
        atkCoolTimeCalc = atkCoolTime;

        boxColliderOffset = boxCollider.offset;
        boxColliderJumpOffset = new Vector2(boxColliderOffset.x, 1f);

        StartCoroutine(FSM());
    }

    IEnumerator FSM()
    {
        while (true)
        {
            yield return StartCoroutine(currentState.ToString());
        }
    }
    IEnumerator Idle()
    {
        boxCollider.offset = boxColliderOffset;
        yield return Delay500;
        currentState = State.Run;
    }
    IEnumerator Run()
    {
        yield return null;
        float runTime = Random.Range(2f, 4f);
        while (runTime >= 0f)
        {
            runTime -= Time.deltaTime;
            if (!isHit)
            {
                MyAnimSetTrigger("Run");
                rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, rb.velocity.y);

                //Mushroom Update
                if (!Physics2D.OverlapCircle(wallCheck[0].position, 0.01f, layerMask) &&
                   Physics2D.OverlapCircle(wallCheck[1].position, 0.01f, layerMask) &&
                    !Physics2D.Raycast(transform.position, -transform.localScale.x * transform.right, 1f, layerMask))
                {
                    currentState = State.Jump;
                    break;
                }
                else if (Physics2D.OverlapCircle(wallCheck[1].position, 0.01f, layerMask))
                {
                    MonsterFlip();
                }

                if (IsPlayerDir() && isGround && canAtk)
                {
                    if (Vector2.Distance(transform.position, PlayerData.Instance.Player.transform.position) < 5f)
                    {
                        currentState = State.Attack;
                        break;
                    }
                }
            }
            yield return null;
        }
        if (currentState != State.Attack &&
             currentState != State.Jump)
        {
            if (!IsPlayerDir())
            {
                MonsterFlip();
            }
        }
    }
    IEnumerator Attack()
    {
        yield return null;
        if (!isHit && isGround)
        {
            boxCollider.offset = boxColliderJumpOffset;
            canAtk = false;
            rb.velocity = new Vector2(-transform.localScale.x * 18f, jumpPower / 1.25f);
            MyAnimSetTrigger("Attack");
            yield return Delay500;
            currentState = State.Idle;
        }
        else
        {
            currentState = State.Run;
        }
    }
    IEnumerator Jump()
    {
        yield return null;
        boxCollider.offset = boxColliderJumpOffset;

        rb.velocity = new Vector2(-transform.localScale.x * 6f, jumpPower);
        MyAnimSetTrigger("Attack");
        yield return Delay500;
        currentState = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        if (!isHit && isGround && !IsPlayingAnim("Run"))
        {
            boxCollider.offset = boxColliderOffset;
            MyAnimSetTrigger("Idle");
        }
    }
}