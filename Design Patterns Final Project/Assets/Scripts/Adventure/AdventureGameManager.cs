using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureGameManager : MonoBehaviour
{
    public static AdventureGameManager Instance { get; private set; }
    public int score = 0;

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

    public void AddScore(int points)
    {
        score += points;
        UIManager.Instance.UpdateScoreText(score);
    }
}
