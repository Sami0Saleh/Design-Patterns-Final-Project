using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _pickupPrefab;
    [SerializeField] private List<Transform> _pickupSpawnPoints;
    [SerializeField] private float _spawnRate = 10f;

    private float _nextSpawnTime;

    // Dictionary to keep track of pickups at each spawn point
    private Dictionary<Transform, GameObject> _spawnedPickups = new Dictionary<Transform, GameObject>();

    void Start()
    {
        _nextSpawnTime = Time.time + _spawnRate;

        foreach (var spawnPoint in _pickupSpawnPoints)
        {
            _spawnedPickups.Add(spawnPoint, null);
        }
    }

    void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            bool spawned = SpawnPickup();

            if (spawned)
            {
                _nextSpawnTime = Time.time + _spawnRate;
            }
            else
            {
                _nextSpawnTime = Time.time + _spawnRate;
            }
        }
    }

    bool SpawnPickup()
    {
        List<Transform> availableSpawnPoints = new List<Transform>();

        foreach (var entry in _spawnedPickups)
        {
            if (entry.Value == null)
            {
                availableSpawnPoints.Add(entry.Key);
            }
        }

        if (availableSpawnPoints.Count == 0)
        {
            return false;
        }

        int spawnIndex = Random.Range(0, availableSpawnPoints.Count);
        Transform spawnPoint = availableSpawnPoints[spawnIndex];

        GameObject newPickup = Instantiate(_pickupPrefab, spawnPoint.position, Quaternion.identity);

        _spawnedPickups[spawnPoint] = newPickup;

        Pickup pickupScript = newPickup.GetComponent<Pickup>();
        pickupScript.SetSpawner(this, spawnPoint);

        return true;
    }

    public void OnPickupCollected(Transform spawnPoint)
    {
        if (_spawnedPickups.ContainsKey(spawnPoint))
        {
            _spawnedPickups[spawnPoint] = null;
        }
    }
}
