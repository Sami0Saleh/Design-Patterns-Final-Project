using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObserver : MonoBehaviour
{
    private int _pickupPoints = 2;
    void OnEnable()
    {
        Pickup.OnPickupCollected += HandlePickupCollected;
    }

    void OnDisable()
    {
        Pickup.OnPickupCollected -= HandlePickupCollected;
    }

    void HandlePickupCollected(Pickup pickup)
    {
        AdventureGameManager.Instance.AddScore(_pickupPoints);
        MinimapManager.Instance.Unregister(pickup.gameObject);
    }
}
