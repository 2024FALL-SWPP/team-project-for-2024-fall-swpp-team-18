using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public Button pauseButton;
    bool isPaused = false;

    public TextMeshProUGUI scoreText; // 연결된 텍스트
    private int score = 0;            // 현재 점수

    void Start()
    {
        pauseButton.gameObject.SetActive(true);
        pauseButton.onClick.AddListener(TogglePause);
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public int GetScore()
    {
        scoreText.text = "Score: " + score; // 점수 업데이트
        return score;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {   
            Time.timeScale = 0;
            Debug.Log("Game Paused");
        }
        else
        {
            Time.timeScale = 1; 
            Debug.Log("Game Resumed");
        }
    }

    void Update()
    {
        GetScore();

        // Esc 키 입력으로 일시정지/재개
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
}