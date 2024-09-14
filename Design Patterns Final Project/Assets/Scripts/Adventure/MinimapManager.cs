using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    public static MinimapManager Instance { get; private set; }

    private Dictionary<GameObject, GameObject> minimapIcons = new Dictionary<GameObject, GameObject>();
    public RectTransform MinimapRectTransform;
    public float worldWidth = 100f; 
    public float worldHeight = 100f;

    private void Awake()
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

    public void Register(GameObject worldObject, GameObject iconPrefab)
    {
        if (!minimapIcons.ContainsKey(worldObject))
        {
            GameObject icon = UIManager.Instance.AddMinimapIcon(iconPrefab);
            minimapIcons.Add(worldObject, icon);
            UpdateIconPosition(worldObject, worldObject.transform.position);
        }
    }

    public void Unregister(GameObject worldObject)
    {
        if (minimapIcons.TryGetValue(worldObject, out GameObject icon))
        {
            UIManager.Instance.RemoveMinimapIcon(icon);
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
            // Get the local position on the minimap for the world position
            Vector3 minimapPosition = ConvertWorldToMinimapPosition(worldPosition);

            // Set the icon's local position relative to the minimap RectTransform
            RectTransform iconRectTransform = icon.GetComponent<RectTransform>();
            iconRectTransform.anchoredPosition = minimapPosition;
        }
    }

    private Vector3 ConvertWorldToMinimapPosition(Vector3 worldPosition)
    {
        // Get the dimensions of the minimap RectTransform
        float minimapWidth = MinimapRectTransform.rect.width;
        float minimapHeight = MinimapRectTransform.rect.height;

        // Scale factors based on world dimensions and minimap dimensions
        float scaleX = minimapWidth / worldWidth;
        float scaleY = minimapHeight / worldHeight;

        // Convert the world position into a normalized value (0 to 1) within the world boundaries
        float normalizedX = (worldPosition.x / worldWidth) + 0.5f; // Normalized X from [-worldWidth/2, worldWidth/2] to [0, 1]
        float normalizedY = (worldPosition.z / worldHeight) + 0.5f; // Normalized Y from [-worldHeight/2, worldHeight/2] to [0, 1]

        // Scale the normalized coordinates to the minimap size
        float minimapX = normalizedX * minimapWidth;
        float minimapY = normalizedY * minimapHeight;

        // Adjust the position so that (0, 0) is the center of the minimap
        minimapX -= minimapWidth / 2;
        minimapY -= minimapHeight / 2;

        // Return the local position within the minimap
        return new Vector3(minimapX, minimapY, 0);
    }
}
