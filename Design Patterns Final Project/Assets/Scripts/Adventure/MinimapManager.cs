using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    public static MinimapManager Instance { get; private set; }

    private Dictionary<GameObject, GameObject> _minimapIcons = new Dictionary<GameObject, GameObject>();
    public RectTransform MinimapRectTransform;
    public float WorldWidth = 100f; 
    public float WorldHeight = 100f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Register(GameObject worldObject, GameObject iconPrefab)
    {
        if (!_minimapIcons.ContainsKey(worldObject))
        {
            GameObject icon = UIManager.Instance.AddMinimapIcon(iconPrefab);
            _minimapIcons.Add(worldObject, icon);
            UpdateIconPosition(worldObject, worldObject.transform.position);
        }
    }

    public void Unregister(GameObject worldObject)
    {
        if (_minimapIcons.TryGetValue(worldObject, out GameObject icon))
        {
            UIManager.Instance.RemoveMinimapIcon(icon);
            _minimapIcons.Remove(worldObject);
        }
    }

    void Update()
    {
        foreach (var entry in _minimapIcons)
        {
            UpdateIconPosition(entry.Key, entry.Key.transform.position);
        }
    }

    private void UpdateIconPosition(GameObject worldObject, Vector3 worldPosition)
    {
        if (_minimapIcons.TryGetValue(worldObject, out GameObject icon))
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

        float normalizedX = (worldPosition.x / WorldWidth * 0.1f) + 0.5f; 
        float normalizedY = (worldPosition.z / WorldHeight * 0.1f) + 0.5f; 

        float minimapX = normalizedX * minimapWidth;
        float minimapY = normalizedY * minimapHeight;

        minimapX -= minimapWidth / 2;
        minimapY -= minimapHeight / 2;

        return new Vector3(minimapX, minimapY, 0);
    }
}
