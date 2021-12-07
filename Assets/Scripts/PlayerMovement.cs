using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    SpriteRenderer sr;
    float refVelocity;

    public float moveDir;
    public float moveSpeed = 4500f;
    public float maxSpeed = 5.5f;
    public float jumpPower = 17f;

    public float slideRate = 0.35f;
    public float AttackSlideRate = 0.25f;

    public LayerMask whatisGround;
    public Animator Anim;

    public bool isGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        GroundCheck();
        PlayerAnim();
        GroundFriction();
    }
    private void FixedUpdate()
    {
        if (!IsPlayingAnim("Attack"))
        {
            if (PlayerFlip() || Mathf.Abs(moveDir * rb.velocity.x) < maxSpeed)
            {
                rb.AddForce(new Vector2(moveDir * Time.fixedDeltaTime * moveSpeed, 0));
            }
            //else //없어도 될것 같다
            //{
            //    rb.velocity = new Vector2 ( moveDir * maxSpeed, rb.velocity.y ); // 이게 업데이트에 들어가면 화면이 안끊길까?
            //}
        }
    }

    bool IsPlayingAnim(string AnimName)
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName(AnimName))
        {
            return true;
        }
        return false;
    }
    void MyAnimSetTrigger(string AnimName)
    {
        if (!IsPlayingAnim(AnimName))
        {
            Anim.SetTrigger(AnimName);
        }
    }

    void PlayerInput()
    {
        moveDir = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGround && !IsPlayingAnim("Attack"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower); // 
            MyAnimSetTrigger("Jump");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            MyAnimSetTrigger("Attack");
        }
        //else if ( Input.GetKeyDown ( KeyCode.UpArrow ) )
        //{
        //  InterAct
        //}
        //else if ( Input.GetKeyDown ( KeyCode.DownArrow ) )
        //{
        //  Monkey Magic~
        //}
    }
    void GroundFriction()
    {
        if (isGround)
        {
            if (IsPlayingAnim("Attack"))
            {
                rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, 0f, ref refVelocity, slideRate + AttackSlideRate), rb.velocity.y);
            }
            else if (Mathf.Abs(moveDir) <= 0.01f)
            {
                rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, 0f, ref refVelocity, slideRate), rb.velocity.y);
            }
        }
    }
    void PlayerAnim()
    {
        if (isGround && !IsPlayingAnim("Attack"))
        {
            if ((Mathf.Abs(moveDir) <= 0.01f || Mathf.Abs(rb.velocity.x) <= 0.01f) && Mathf.Abs(rb.velocity.y) <= 0.01f)
            {
                MyAnimSetTrigger("Idle");
            }
            else if (Mathf.Abs(rb.velocity.x) > 0.01 && Mathf.Abs(rb.velocity.y) <= 0.01f)
            {
                MyAnimSetTrigger("Walk");
            }
        }
    }
    bool PlayerFlip()
    {
        bool flipSprite = (sr.flipX ? (moveDir > 0f) : (moveDir < 0f));
        if (flipSprite)
        {
            sr.flipX = !sr.flipX;
            GroundFriction();
        }
        return flipSprite;
    }
    void GroundCheck()
    {
        if (Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.down, 0.01f, whatisGround))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
}