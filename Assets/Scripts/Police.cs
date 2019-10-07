using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Vector2 moveSpeedInterval;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;

    private float moveSpeed = 1f;
    private bool arresting = false;
    private int direction = 1;

    private void Start()
    {
        moveSpeed = Random.Range(moveSpeedInterval.x, moveSpeedInterval.y);
    }

    void Update()
    {
        Move();
    }

    public void SetDirection(int newDirection)
    {
        direction = newDirection;
        if(direction != 1)
        {
            sr.flipX = true;
        }
        
    }

    public void Move()
    {
        if (!arresting)
        {
            rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + Vector2.right * direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (!arresting && player != null)
        {
            Arrest(player);
        }
    }

    private void Arrest(PlayerController player)
    {
        Debug.Log("[Police] Arresting player");

        player.Arrest();
        arresting = true;
        if(player.transform.position.x > transform.position.x)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
        animator.SetBool("isArresting", true);
    }
}
