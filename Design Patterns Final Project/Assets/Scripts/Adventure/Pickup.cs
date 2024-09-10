using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public delegate void PickupCollected(Pickup pickup);
    public static event PickupCollected OnPickupCollected;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickupCollected?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
