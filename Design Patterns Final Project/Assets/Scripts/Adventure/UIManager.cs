using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TMP_Text scoreText;
    public RectTransform minimap;
    

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

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }
    public GameObject AddMinimapIcon(GameObject iconPrefab)
    {
        GameObject icon = Instantiate(iconPrefab, minimap);
        return icon;
    }

    public void RemoveMinimapIcon(GameObject icon)
    {
        Destroy(icon);
    }

}
