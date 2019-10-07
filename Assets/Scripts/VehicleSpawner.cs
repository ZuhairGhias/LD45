using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{

    [SerializeField] private Transform spawnLeft;
    [SerializeField] private Transform spawnRight;
    [SerializeField] private Vector2 spawnIntervals;
    [SerializeField] private Vector2 speedInterval;
    [SerializeField] private List<Vehicle> vehicles;
    [SerializeField] private Transform vehicleParent;
    
    void Start()
    {
        StartCoroutine(VehicleSpawnRoutine());
    }

    private IEnumerator VehicleSpawnRoutine() {

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervals.x, spawnIntervals.y));

            Vehicle vehicle = vehicles[Random.Range(0, vehicles.Count)];

            if (Random.Range(0f, 1f) < 0.5f)
            {
                Vehicle car = Instantiate(vehicle, spawnLeft.position, Quaternion.identity);
                car.SetDirection(Random.Range(speedInterval.x, speedInterval.y));
                car.transform.SetParent(vehicleParent);
            }
            else
            {
                Vehicle car = Instantiate(vehicle, spawnRight.position, Quaternion.identity);
                car.SetDirection(-Random.Range(speedInterval.x, speedInterval.y));
                car.transform.SetParent(vehicleParent);
            }
        }
    }
}
