using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    public static Action<int> OnPickpocket;

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Vector2 moveSpeedInterval;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color pickpocketedColor;

    private float moveSpeed = 6f;
    private bool pickpocketed = false;
    private int direction = 1;

    private void Start()
    {
        moveSpeed = UnityEngine.Random.Range(moveSpeedInterval.x, moveSpeedInterval.y);
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
        rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + Vector2.right * direction * moveSpeed * Time.deltaTime);
    }

    public int Pickpocket()
    {
        if (!pickpocketed)
        {
            Debug.Log("[Pedestrian] Pedestrian pickpocketed!");

            int moneyStolen = UnityEngine.Random.Range(1, 5);
            spriteRenderer.color = pickpocketedColor;
            pickpocketed = true;

            OnPickpocket(moneyStolen);

            return moneyStolen;
        }

        return 0;
    }
}
