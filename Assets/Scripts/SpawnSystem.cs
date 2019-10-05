using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private Pedestrian pedestrianPrefab;
    [SerializeField] private Police policePrefab;

    [SerializeField] private Transform spawnPointLeft;
    [SerializeField] private Transform spawnPointRight;

    private bool active = true;

    public void SpawnPolice(int direction)
    {
        Police police;

        switch (direction)
        {
            case -1:
                police = Instantiate(policePrefab, spawnPointRight.position, Quaternion.identity);
                police.SetDirection(direction);
                break;

            case 1:
                police = Instantiate(policePrefab, spawnPointLeft.position, Quaternion.identity);
                police.SetDirection(direction);
                break;

            default:
                Debug.LogError("[SpawnSystem] Invalid direction input in SpawnPolice");
                break;
        }
    }

    public void SpawnPedestrian(int direction)
    {
        Pedestrian pedestrian;

        switch (direction)
        {
            case -1:
                pedestrian = Instantiate(pedestrianPrefab, spawnPointRight.position, Quaternion.identity);
                pedestrian.SetDirection(direction);
                break;

            case 1:
                pedestrian = Instantiate(pedestrianPrefab, spawnPointLeft.position, Quaternion.identity);
                pedestrian.SetDirection(direction);
                break;

            default:
                Debug.LogError("[SpawnSystem] Invalid direction input in SpawnPedestrian");
                break;
        }
    }
}
