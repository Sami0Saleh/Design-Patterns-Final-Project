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
            Vector3 minimapPosition = ConvertWorldToMinimapPosition(worldPosition);

            RectTransform iconRectTransform = icon.GetComponent<RectTransform>();
            iconRectTransform.anchoredPosition = minimapPosition;
        }
    }

    private Vector3 ConvertWorldToMinimapPosition(Vector3 worldPosition)
    {
        float minimapWidth = MinimapRectTransform.rect.width;
        float minimapHeight = MinimapRectTransform.rect.height;

        float normalizedX = (worldPosition.x / worldWidth * 0.1f) + 0.5f; 
        float normalizedY = (worldPosition.z / worldHeight * 0.1f) + 0.5f; 

        float minimapX = normalizedX * minimapWidth;
        float minimapY = normalizedY * minimapHeight;

        minimapX -= minimapWidth / 2;
        minimapY -= minimapHeight / 2;

        return new Vector3(minimapX, minimapY, 0);
    }
}
