using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Pedestrian>() ||
            collision.gameObject.GetComponent<Police>() ||
            collision.gameObject.GetComponent<Vehicle>())
        {
            Destroy(collision.gameObject);
        }
    }
}
