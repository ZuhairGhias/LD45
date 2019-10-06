using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{

    private float direction = 1;
    [SerializeField] SpriteRenderer sr;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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
    }

    public void Move()
    {
        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + Vector2.right * direction * Time.deltaTime);
    }
}
