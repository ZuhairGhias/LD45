using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    [SerializeField] private bool pickpocketed = false;
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
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);
    }

    public int Pickpocket()
    {
        Debug.Log("[Pedestrian] Pedestrian pickpocketed!");

        pickpocketed = true;
        return Random.Range(1, 5);
    }
}
