using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour
{
    [SerializeField] private bool arresting = false;
    [SerializeField] private float moveSpeed = 6f;

    private int direction = 1;

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
            transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);
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
