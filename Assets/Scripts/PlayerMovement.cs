using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PlayerMovement.cs, YeongjinLim, 101256371, last Modified in 2021/11/20, Basicmovement for player, revision history - #01 added basic movement
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    float moveDir;
    public float moveSpeed = 250f;
    public float jumpPower = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown (KeyCode.Space))
        {
            rb.velocity += new Vector2(0, jumpPower);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(moveDir * Time.fixedDeltaTime * moveSpeed, rb.velocity.y));
    }
}
