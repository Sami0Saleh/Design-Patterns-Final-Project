using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObserver : MonoBehaviour
{
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
        AdventureGameManager.Instance.AddScore(10);
        // Remove the pickup from the minimapRectTransform
        // MinimapManager.Instance.RemovePickupMarker(pickup);
    }
}
