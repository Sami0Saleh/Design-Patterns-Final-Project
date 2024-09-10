using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    public static MinimapManager Instance { get; private set; }

    private Dictionary<GameObject, GameObject> minimapIcons = new Dictionary<GameObject, GameObject>();
    private RectTransform minimapRectTransform;
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
    private void Start()
    {
        minimapRectTransform = UIManager.Instance.minimapRectTransform;
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
            icon.transform.localPosition = minimapPosition;
        }
    }

    private Vector3 ConvertWorldToMinimapPosition(Vector3 worldPosition)
    {
        var minimapSize = minimapRectTransform.rect.size;
        var worldsize = new Vector2 (worldPosition.x, worldPosition.y);

        var trans = -minimapSize / 2;
        var scale = minimapSize / worldsize;


        float minimapWidth = minimapRectTransform.rect.width;
        float minimapHeight = minimapRectTransform.rect.height;

        float scaleX = minimapWidth / worldWidth;
        float scaleY = minimapHeight / worldHeight;

        float minimapX = (worldPosition.x + worldWidth / 2) * scaleX;
        float minimapY = (worldPosition.z + worldHeight / 2) * scaleY;

        return new Vector3(minimapX - minimapWidth / 2, minimapY - minimapHeight / 2, 0);
    }
}
