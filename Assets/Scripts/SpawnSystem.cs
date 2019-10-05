using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public Pedestrian ped;
    public Transform SpawnPointLeft;
    public Transform SpawnPointRight;


    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        Instantiate(ped, SpawnPointLeft.transform.position, Quaternion.identity);
    }
}
