using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TMP_Text scoreText;
    public RectTransform minimapRectTransform;
    

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
    private void Start()
    {
        if (minimapRectTransform == null && Instance != null)
        {
            minimapRectTransform = Instance.minimapRectTransform;
        }
    }
    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }
    public GameObject AddMinimapIcon(GameObject iconPrefab)
    {
        GameObject icon = Instantiate(iconPrefab, minimapRectTransform);
        return icon;
    }

    public void RemoveMinimapIcon(GameObject icon)
    {
        Destroy(icon);
    }

}
