using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Transform[] WeaponPosition; //위치 3개

    public GameObject hitBoxCollider;
    public GameObject shield;
    public GameObject weaponCollider;

    public SpriteRenderer[] sprites;

    public float hitRecovery = 3.0f;
    public bool equipShield;
    public bool equipWeapon;
    bool invincible;

    private void Awake()
    {
        StartCoroutine(ResetCollider());
    }

    IEnumerator ResetCollider()
    {
        while (true)
        {
            yield return null;
            if (!hitBoxCollider.activeInHierarchy)
            {
                yield return new WaitForSeconds(hitRecovery);
                hitBoxCollider.SetActive(true);
            }
        }
    }
    IEnumerator InvincibleEffect()
    {
        
        invincible = true; //무적    

        foreach (SpriteRenderer SR in sprites)
        {
            SR.color = new Color(1f, 1f, 1f, 0.4f);
        }
        yield return new WaitForSeconds(1f);

        invincible = false; // 무적해제   

        foreach (SpriteRenderer SR in sprites)
        {
            SR.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)  //damaged
    {
        if (collision.transform.CompareTag("Monster") || collision.transform.CompareTag("Projectile"))
        {
            if (collision.transform.CompareTag("Projectile"))
            {
                Destroy(collision.gameObject, 0.02f);
            }
            if (!invincible)
            {
                StartCoroutine(InvincibleEffect());
                Debug.Log("HP-- ");
            }

            playerMovement.rb.velocity = Vector2.zero;
            playerMovement.isHit = true;
            Invoke("isHitReset", 0.5f);
            hitBoxCollider.SetActive(false);
            if (transform.position.x > collision.transform.position.x)
            {
                playerMovement.rb.AddForce(new Vector2(70, 140), ForceMode2D.Impulse);
            }
            else
            {
                playerMovement.rb.AddForce(new Vector2(-70, 140), ForceMode2D.Impulse);
            }

            if (!playerMovement.IsPlayingAnim("Attack"))
            {
                playerMovement.MyAnimSetTrigger("DMG");
            }
        }
    }


    void isHitReset()
    {
        playerMovement.isHit = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)   //defence
    {
        if (collision.transform.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject, 0.02f);
        }
    }

    public void WeaponColliderOnOff()
    {
        if (equipWeapon)
        {
            weaponCollider.SetActive(!weaponCollider.activeInHierarchy);
        }
    }
    public void ShieldColliderOnOff()
    {
        if (equipShield)
        {
            shield.SetActive(!shield.activeInHierarchy);
        }
    }
    public void EquipShoe(int index)
    {
    }
    public void EquipShield(bool equip)
    {
        equipShield = equip;
        shield.SetActive(equip);
    }
    public void EquipWeapon(int index)
    {
        switch (index)
        {
            case -1:
                equipWeapon = false;
                break;
            case 0:
            case 1:
            case 2:
                weaponCollider.transform.position = WeaponPosition[index].position;
                equipWeapon = true;
                break;
            case 3:
            case 4:
                weaponCollider.transform.position = WeaponPosition[2].position;
                equipWeapon = true;
                break;
        }
    }
}