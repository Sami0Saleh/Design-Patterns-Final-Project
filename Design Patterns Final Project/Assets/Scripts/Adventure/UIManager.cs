using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject _pauseMenu;
    public TMP_Text ScoreText;
    

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
    
    public void UpdateScoreText(int score)
    {
        ScoreText.text = "Score: " + score;
    }
    public GameObject AddMinimapIcon(GameObject iconPrefab)
    {
        GameObject icon = Instantiate(iconPrefab, MinimapManager.Instance.MinimapRectTransform);
        return icon;
    }

    public void RemoveMinimapIcon(GameObject icon)
    {
        Destroy(icon);
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
    }

    public void Continue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
        _pauseMenu.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
