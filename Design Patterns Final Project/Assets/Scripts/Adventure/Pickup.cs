using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public delegate void PickupCollected(Pickup pickup);
    public static event PickupCollected OnPickupCollected;

    private PickupSpawner _spawner;
    private Transform _spawnPoint;

    public void SetSpawner(PickupSpawner spawner, Transform spawnPoint)
    {
        _spawner = spawner;
        _spawnPoint = spawnPoint;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickupCollected?.Invoke(this);
            _spawner.OnPickupCollected(_spawnPoint);
            Destroy(gameObject);
        }
    }
}
