using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private Pedestrian pedestrianPrefab;
    [SerializeField] private Police policePrefab;

    [SerializeField] private Transform spawnPointLeft;
    [SerializeField] private Transform spawnPointRight;
    
    [SerializeField] private Vector2 spawnIntervals;
    [SerializeField] private float basePoliceSpawnChance = 0f;
    [SerializeField] private float maxPoliceSpawnChance = 0.35f;
    
    private GameManager gameManager;
    private Coroutine spawnCoroutine;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartSpawning()
    {
        spawnCoroutine = StartCoroutine(SpawnNPCs());
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

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
    
    private IEnumerator SpawnNPCs()
    {
        while(true)
        {
            // Randomize movement direction
            int direction = 1;
            if (Random.Range(0f, 1f) < 0.5f)
            {
                direction = -1;
            }

            // Randomize NPC type
            float policeSpawnChance = Mathf.Clamp(basePoliceSpawnChance + gameManager.GetCurrentHeat() / 2f, 0f, maxPoliceSpawnChance);
            if (Random.Range(0f, 1f) <= policeSpawnChance)
            {
                SpawnPolice(direction);
            }
            else
            {
                SpawnPedestrian(direction);
            }

            yield return new WaitForSeconds(Random.Range(spawnIntervals.x, spawnIntervals.y));
        }
    }
}
