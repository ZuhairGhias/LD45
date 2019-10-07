using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDespawn : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Vehicle>())
        {
            Destroy(collision.gameObject);
        }
    }
            
}
