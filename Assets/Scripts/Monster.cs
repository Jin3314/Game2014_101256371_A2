using System.Collections;
using UnityEngine;

/*
 * Monster.cs
 * Made by YeongjinLim 101256371
 * Last modified in 2021-12-12
    Script for Monster
 */
public class Monster : MonoBehaviour
{
    public int currentHp = 5;
    public float moveSpeed = 5f;
    public float jumpPower = 10;
    public float atkCoolTime = 3f;
    public float atkCoolTimeCalc = 3f;

    public bool isHit = false;
    public bool isGround = true;
    public bool canAtk = true;
    public bool MonsterDirRight;

    protected Rigidbody2D rb;
    protected BoxCollider2D boxCollider;
    public GameObject hitBoxCollider;
    public Animator Anim;
    public LayerMask layerMask;

    AudioSource audioSource;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        Anim = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        StartCoroutine(CalcCoolTime());
        StartCoroutine(ResetCollider());
    }

    IEnumerator ResetCollider()
    {
        while (true)
        {
            yield return null;
            if (!hitBoxCollider.activeInHierarchy)
            {
                yield return new WaitForSeconds(0.5f);
                hitBoxCollider.SetActive(true);
                isHit = false;
            }
        }
    }
    IEnumerator CalcCoolTime()
    {
        while (true)
        {
            yield return null;
            if (!canAtk)
            {
                atkCoolTimeCalc -= Time.deltaTime;
                if (atkCoolTimeCalc <= 0)
                {
                    atkCoolTimeCalc = atkCoolTime;
                    canAtk = true;
                }
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
        }
    }

    protected void MonsterFlip()
    {
        MonsterDirRight = !MonsterDirRight;

        Vector3 thisScale = transform.localScale;
        if (MonsterDirRight)
        {
            thisScale.x = -Mathf.Abs(thisScale.x);
        }
        else
        {
            thisScale.x = Mathf.Abs(thisScale.x);
        }
        transform.localScale = thisScale;
        rb.velocity = Vector2.zero;
    }

    protected bool IsPlayerDir()
    {
        if (transform.position.x < PlayerData.Instance.Player.transform.position.x ? MonsterDirRight : !MonsterDirRight)
        {
            return true;
        }
        return false;
    }

    protected void GroundCheck()
    {
        if (Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.down, 0.05f, layerMask))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    public void TakeDamage(int dam)
    {

        dam = 1;
        currentHp -= dam;
        isHit = true;
        // Knock Back or Dead

        if(currentHp <= 0)
        {
            audioSource.Play();
            Debug.Log("Monster Dead");
            Destroy(gameObject);

            PlayerAction playerAction = GameObject.Find("PLAYER").GetComponent<PlayerAction>();
            playerAction.score += 10;
        }
        else
        {
            audioSource.Play();
            MyAnimSetTrigger("Hit");
            rb.velocity = Vector2.zero;
            if(transform.position.x > PlayerData.Instance.Player.transform.position.x)
            {
                rb.velocity = new Vector2(10f, 0);
            }
            else
            {
                rb.velocity = new Vector2(-10f, 0);
            }
        }

        hitBoxCollider.SetActive(false);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.transform.CompareTag ("PlayerWeapon" ) )
        {
          TakeDamage ( 0 );
        }
    }

}