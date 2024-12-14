using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리에 필요

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject scoreManager;
    public ScoreManager scoreManagerScript;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public bool GameOver = false;

    void Update()
    {
        if (GameOver)
        {
            HandleGameOver();
        }
    }

    public void HandleGameOver()
    {
        Debug.Log("Game Over! Returning to Main Menu...");

        SceneManager.LoadScene("Main"); // 예: 메인 메뉴 씬으로 이동
    }

    public void HandleGameClear()
    {
        Debug.Log("Game Clear! Returning to Main Menu...");
        SceneManager.LoadScene("Main");
    }
}
