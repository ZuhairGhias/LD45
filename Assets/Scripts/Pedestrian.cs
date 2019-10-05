using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    public bool looted;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        looted = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move(1);
    }

    public int Loot()
    {
        looted = true;
        print("ped looted");
        return 10;
    }

    public void Move(float amount)
    {
        transform.Translate(Vector2.right * amount * moveSpeed * Time.deltaTime);
    }
}
