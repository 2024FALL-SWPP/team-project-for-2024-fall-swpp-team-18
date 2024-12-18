using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리에 필요

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool isGameOver = false;
    public bool isGameClear = false;
    public bool isPaused = false;
    public float grade = 0.0f;
    public int student = 0;
    public float time = 0.0f;
    public int total = 0;
    public int professor = 0;
    public int gradeNum = 0;
    public bool isEasy = true;

    public class OverBy
    {
        public const int Avalanche = 0;
        public const int Snowball = 1;
        public const int Obstacle = 2;
    }

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

    void Update() { }

    public void HandleGameOver(
        int overtype,
        float _grade,
        int _student,
        float _time,
        int _total,
        int _professor,
        int _gradeNum
    )
    {
        grade = _grade;
        student = _student;
        time = _time;
        total = _total;
        professor = _professor;
        gradeNum = _gradeNum;

        SFXController.PlayExplosion();
        isGameOver = true;
        Debug.Log("Game Over! Returning to Main Menu...");
        BackgroundMusicController.Instance.PlayGameOverMusic();

        if (overtype == OverBy.Avalanche)
        {
            SceneManager.LoadScene("AvalancheOutro");
        }
        else if (overtype == OverBy.Snowball)
        {
            SceneManager.LoadScene("SnowballOutro");
        }
        else if (overtype == OverBy.Obstacle)
        {
            SceneManager.LoadScene("AvalancheOutro");
        }
        else
        {
            SceneManager.LoadScene("Outro1");
        }
        // SceneManager.LoadScene("Main"); // 예: 메인 메뉴 씬으로 이동
    }

    public void HandleGameClear(
        float _grade,
        int _student,
        float _time,
        int _total,
        int _professor,
        int _gradeNum
    )
    {
        grade = _grade;
        student = _student;
        time = _time;
        total = _total;
        professor = _professor;
        gradeNum = _gradeNum;

        isGameClear = true;
        Debug.Log("Game Clear! Returning to Main Menu...");
        SceneManager.LoadScene("Outro1");
    }
}
