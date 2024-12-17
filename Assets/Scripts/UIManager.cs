using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    private GameObject scoreManager;
    private ScoreManager scoreManagerScript;

    public Button pauseButton;
    bool isPaused = false;

    public TextMeshProUGUI scoreText; // 연결된 텍스트
    private int score = 0; // 현재 점수

    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager");
        scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
        pauseButton.gameObject.SetActive(true);
        pauseButton.onClick.AddListener(TogglePause);
    }

    public int GetScore()
    {
        scoreText.text = "Score: " + scoreManagerScript.CalculateTotal(); // 점수 업데이트
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
        if (!GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {
            GetScore();
        }
        // Esc 키 입력으로 일시정지/재개
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
}
