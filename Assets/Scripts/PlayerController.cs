using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float stealRate;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float amount)
    {
        //rb.MovePosition(rb.position + (Vector2.right * amount * moveSpeed * Time.deltaTime));
        transform.Translate(Vector2.right * amount * moveSpeed * Time.deltaTime);
    }

    public void Steal()
    {
        print("Initiating steal");
    }
}
