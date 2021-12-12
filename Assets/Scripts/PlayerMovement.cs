using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    BoxCollider2D boxCollider;
    SpriteRenderer sr;
    float refVelocity;

    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;
    AudioSource audioSource;

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
        this.audioSource = GetComponent<AudioSource>();
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
            PlaySound("JUMP");
            Jump = false;
        }
        if (Attack)
        {
            MyAnimSetTrigger("Attack");
            PlaySound("ATTACK");
        }
    }
    private void FixedUpdate()
    {
        if(IsPlayingAnim("Dmg") || isHit)
        {
            PlaySound("DAMAGED");
            rb.velocity *= 0.78f;
            return;
        }
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
            /* else
            {
                rb.velocity = new Vector2(moveDir * maxSpeed, rb.velocity.y);
            }*/

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)  //damaged
    {
        if (collision.gameObject.tag == "Item")
        {
            //Point
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");
            bool isEmerald = collision.gameObject.name.Contains("Emerald");

            if (isSilver)
            {
                PlayerAction playerAction = GameObject.Find("PLAYER").GetComponent<PlayerAction>();
                playerAction.score += 50;
            }
            else if (isGold)
            {
                PlayerAction playerAction = GameObject.Find("PLAYER").GetComponent<PlayerAction>();
                playerAction.score += 100;
            }
            else if (isEmerald)
            {
                PlayerAction playerAction = GameObject.Find("PLAYER").GetComponent<PlayerAction>();
                playerAction.score += 200;
            }
            //Sound
            PlaySound("ITEM");
            //Deactive Item
            collision.gameObject.SetActive(false);
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
        if(IsPlayingAnim("Dmg") || isHit)
        {
            return;
        }
        moveDir = Input.GetAxisRaw("Horizontal");
       

        if (Input.GetKeyDown(KeyCode.Space) && isGround && !IsPlayingAnim("Attack"))
        {

            rb.velocity = new Vector2(rb.velocity.x, jumpPower); // 
            MyAnimSetTrigger("Jump");

            PlaySound("JUMP");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            MyAnimSetTrigger("Attack");
            PlaySound("ATTACK");
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
        if (isGround && !IsPlayingAnim("Attack") && !IsPlayingAnim("Dmg"))
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
        RaycastHit2D GroundBoxHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.3f, whatisGround);

        if (GroundBoxHit &&
            (GroundBoxHit.collider.transform.CompareTag("MovingPlatform") || GroundBoxHit.collider.transform.CompareTag("OneWayPlatform")))
        {
            isGround = (GroundBoxHit.collider.bounds.max.y <= boxCollider.bounds.min.y + 0.1f);
        }
        else
        {
            isGround = GroundBoxHit;
        }

        Color rayColor;
        rayColor = isGround ? Color.green : Color.red;
        Debug.DrawRay(new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.center.y - boxCollider.bounds.extents.y), Vector2.down * (0.1f), rayColor);
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;
        }
        audioSource.Play();
    }
}