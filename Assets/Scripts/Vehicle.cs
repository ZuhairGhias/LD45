using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{

    private float direction = 1;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] AudioSource audioSource;

    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Move();
    }

    public void SetDirection(float direction)
    {
        if(direction < 0)
        {
            sr.flipX = !sr.flipX;
        }

        this.direction = direction;
        audioSource.pitch = Random.Range(0.5f, 0.8f);
    }

    public void Move()
    {
        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + Vector2.right * direction * Time.fixedDeltaTime);
    }
}
