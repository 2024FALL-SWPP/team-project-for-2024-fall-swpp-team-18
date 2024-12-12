using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리에 필요

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

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

    void HandleGameOver()
    {
        // 게임 종료 동작 실행
        Debug.Log("Game Over! Returning to Main Menu...");
        SceneManager.LoadScene("MainMenu"); // 예: 메인 메뉴 씬으로 이동
    }
}
