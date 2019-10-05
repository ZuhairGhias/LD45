using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour
{
    public float moveSpeed;

    private bool arresting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(1);
    }

    public void Move(float amount)
    {
        if (arresting) return;
        transform.Translate(Vector2.right * amount * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null) Arrest(player);
    }

    private void Arrest(PlayerController player)
    {
        print("Arrest Player");
        arresting = true;
        player.Arrest();
    }
}
