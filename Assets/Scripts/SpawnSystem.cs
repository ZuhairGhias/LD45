using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public int spawnRate;
    public Pedestrian ped;
    public Police pol;
    public Transform SpawnPointLeft;
    public Transform SpawnPointRight;


    // Start is called before the first frame update
    void Start()
    {
        SpawnPolice();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPolice()
    {
        Instantiate(pol, SpawnPointLeft.transform.position, Quaternion.identity);
    }

    public void SpawnPedestrian()
    {
        Instantiate(ped, SpawnPointLeft.transform.position, Quaternion.identity);
    }
}
