using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Vector2 moveSpeedInterval;

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
    }

    public void Move()
    {
        if (!arresting)
        {
            rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + Vector2.right * direction * moveSpeed * Time.deltaTime);
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
    }
}
