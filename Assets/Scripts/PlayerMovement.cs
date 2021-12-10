using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
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

    public bool LeftMove = false;
    public bool RightMove = false;
    public bool Jump = false;
    public bool isJump = false;

    public bool Attack = false;

    public bool isHit = false;
    Vector3 moveVelocity = Vector3.zero;

    float movespeed = 4;
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

        if (LeftMove == true)
        {

            moveDir = -1;
           

        }
        if (RightMove == true)
        {
            moveDir = 1;
        }

        if (Jump && isGround && !IsPlayingAnim("Attack"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower); // 
            MyAnimSetTrigger("Jump");
            Jump = false;
        }
        if (Attack)
        {
            MyAnimSetTrigger("Attack");
        }
    }
    private void FixedUpdate()
    {
        if (!IsPlayingAnim("Attack"))
        {
            if (PlayerFlip() || Mathf.Abs(moveDir * rb.velocity.x) < maxSpeed)
            {
                if(moveDir == -1)
                {
                    rb.AddForce(new Vector2(moveDir * Time.fixedDeltaTime * moveSpeed, 0));
                }
                if (moveDir == 1)
                {
                    rb.AddForce(new Vector2(moveDir * Time.fixedDeltaTime * moveSpeed, 0));
                }
            }
            else
            {
                rb.velocity = new Vector2(moveDir * maxSpeed, rb.velocity.y);
            }
        }
    }

    public bool IsPlayingAnim(string AnimName)
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName(AnimName))
        {
            return true;
        }
        return false;
    }
    public void MyAnimSetTrigger(string AnimName)
    {
        if (!IsPlayingAnim(AnimName))
        {
            Anim.SetTrigger(AnimName);
            PlayerSkinControl.Instance.SkinSetTrigger(AnimName);
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
        //else if ( rb.velocity.y < 0 && !IsPlayingAnim ( "Jump" ) ) 
        //{
        //    MyAnimSetTrigger ( "Down" );
        //}
    }
    bool PlayerFlipX;
    bool PlayerFlip()
    {
        bool flipSprite = (PlayerFlipX ? (moveDir > 0f) : (moveDir < 0f));
        if (flipSprite)
        {
            PlayerFlipX = !PlayerFlipX;

            Vector3 thisScale = transform.localScale;
            if (PlayerFlipX)
            {
                thisScale.x = -Mathf.Abs(thisScale.x);
            }
            else
            {
                thisScale.x = Mathf.Abs(thisScale.x);
            }
            transform.localScale = thisScale;
            GroundFriction();
        }
        return flipSprite;
    }
    void GroundCheck()
    {
        if (Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.down, 0.01f, whatisGround))
        {
            isGround = true;
            //Anim.ResetTrigger ( "Idle" );
        }
        else
        {
            isGround = false;
        }
    }
}