using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool isUpDown;
    public float movingSpeed;
    public float amplitude;
    Vector2 startPosition;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (isUpDown)
        {
            rb.transform.position = new Vector2(startPosition.x, startPosition.y + amplitude * Mathf.Sin(Time.time * movingSpeed));
        }
        else
        {
            rb.transform.position = new Vector2(startPosition.x + amplitude * Mathf.Sin(Time.time * movingSpeed), startPosition.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && collision.gameObject.GetComponent<PlayerMovement>().isGround)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}