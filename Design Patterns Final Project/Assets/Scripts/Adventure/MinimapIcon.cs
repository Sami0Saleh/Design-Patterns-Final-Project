using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour
{
    [SerializeField] private GameObject minimapIconPrefab;
    private GameObject minimapIcon;

    void Start()
    {
        minimapIcon = Instantiate(minimapIconPrefab, MinimapManager.Instance.transform);
        UIManager.Instance.AddMinimapIcon(minimapIcon);
    }

    public void UpdatePosition(Vector3 worldPosition)
    {
        // This will be handled by the MinimapManager instead
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
