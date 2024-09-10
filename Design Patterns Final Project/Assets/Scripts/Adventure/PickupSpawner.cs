using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pickupPrefab;
    [SerializeField] private List<Transform> pickupSpawnPoints;
    [SerializeField] private float spawnRate = 5f;    

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + spawnRate;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnPickup();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnPickup()
    {
        if (pickupSpawnPoints.Count == 0)
            return;

        int spawnIndex = Random.Range(0, pickupSpawnPoints.Count);
        Transform spawnPoint = pickupSpawnPoints[spawnIndex];

        Instantiate(pickupPrefab, spawnPoint.position, Quaternion.identity);
    }
}
