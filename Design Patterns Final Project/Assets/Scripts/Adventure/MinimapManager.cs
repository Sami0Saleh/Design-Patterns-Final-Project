using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    public static MinimapManager Instance { get; private set; }
    private Dictionary<GameObject, GameObject> minimapIcons = new Dictionary<GameObject, GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Register(GameObject worldObject, GameObject minimapIcon)
    {
        if (!minimapIcons.ContainsKey(worldObject))
        {
            minimapIcons.Add(worldObject, minimapIcon);
            UpdateIconPosition(worldObject, worldObject.transform.position);
        }
    }

    public void Unregister(GameObject worldObject)
    {
        if (minimapIcons.ContainsKey(worldObject))
        {
            Destroy(minimapIcons[worldObject]);
            minimapIcons.Remove(worldObject);
        }
    }

    void Update()
    {
        foreach (var entry in minimapIcons)
        {
            UpdateIconPosition(entry.Key, entry.Key.transform.position);
        }
    }

    private void UpdateIconPosition(GameObject worldObject, Vector3 worldPosition)
    {
        if (minimapIcons.TryGetValue(worldObject, out GameObject icon))
        {
            // Convert world position to minimap position here
            Vector3 minimapPosition = ConvertWorldToMinimapPosition(worldPosition);
            icon.transform.position = minimapPosition;
        }
    }

    private Vector3 ConvertWorldToMinimapPosition(Vector3 worldPosition)
    {
        // Implement conversion logic based on your world and minimap scale
        return new Vector3(worldPosition.x, worldPosition.y, 0); // Simplified example
    }
}
