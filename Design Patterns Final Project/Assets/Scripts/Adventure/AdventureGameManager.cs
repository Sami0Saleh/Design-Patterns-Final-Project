using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureGameManager : MonoBehaviour
{
    public static AdventureGameManager Instance { get; private set; }
    public int Score = 0;

    void Awake()
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

    public void AddScore(int points)
    {
        Score += points;
        UIManager.Instance.UpdateScoreText(Score);
    }
}
