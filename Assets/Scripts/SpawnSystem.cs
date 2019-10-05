using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField]
    private float spawnDelay = 1;
    public Pedestrian ped;
    public Police pol;
    public Transform SpawnPointLeft;
    public Transform SpawnPointRight;

    private bool active = true;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPolice(Transform point, float speedModifier)
    {
        Police newPol = Instantiate(pol, point.position, Quaternion.identity);
        newPol.moveSpeed *= speedModifier;
    }

    public void SpawnPedestrian(Transform point, float speedModifier)
    {
        Pedestrian newPed = Instantiate(ped, point.position, Quaternion.identity);
        newPed.moveSpeed *= speedModifier;
    }

    private IEnumerator SpawnRoutine()
    {
        while (active)
        {
            int rand = Random.Range(0, 5);
            GameObject npc = null;
            if(rand == 0)
            {
                SpawnPedestrian(SpawnPointLeft, 1);
            }
            else if(rand == 1)
            {
                SpawnPolice(SpawnPointLeft, 1);
            }
            else if(rand == 2)
            {
                SpawnPedestrian(SpawnPointRight, -1);
            }
            else
            {
                SpawnPolice(SpawnPointRight, -1);
            }

            rand = Random.Range(0, 2);

            yield return new WaitForSeconds(spawnDelay);
        }
    }



}
