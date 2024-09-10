using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour, IMinimapObserver
{
    [SerializeField] private GameObject _minimapIconPrefab;

    void Start()
    {
        MinimapManager.Instance.Register(gameObject, _minimapIconPrefab);
    }

    public void RemoveFromMinimap()
    {
        MinimapManager.Instance.Unregister(gameObject);
    }

    void OnDestroy()
    {
        RemoveFromMinimap();
    }
}
