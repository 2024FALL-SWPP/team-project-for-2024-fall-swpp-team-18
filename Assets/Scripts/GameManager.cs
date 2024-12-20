using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리에 필요

public enum State
{
    Ready,
    Play,
    GameOver,
    GameClear,
    Pause,
}

public enum OverBy
{
    Avalanche,
    Snowball,
    Obstacle,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public State gameState = State.Ready;
    private State prevState = State.Ready;
    public Stats gameStat = new Stats(0, 0, 0, 0, 0, 0);
    private bool isEasy = true;
    public bool isTest = false;

    /*
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
        해당 부분 아래와 같이 수정*/
    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // Singleton 패턴 초기화
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        checkStateChange();
    }

    public State getState()
    {
        return this.gameState;
    }

    public void setState(State newState)
    {
        this.gameState = newState;
    }

    public bool ifEasy()
    {
        return this.isEasy;
    }

    public void setEasy(bool easiness)
    {
        this.isEasy = easiness;
    }

    public void HandleGameOver(OverBy overType, Stats overStat)
    {
        gameStat.setTime(overStat.getTime());
        gameStat.setGrade(overStat.getGrade());
        gameStat.setGradeNum(overStat.getGradeNum());
        gameStat.setStudent(overStat.getStudent());
        gameStat.setProfessor(overStat.getProfessor());
        gameStat.setTotal(overStat.getTotal());

        this.setState(State.GameOver);
        Debug.Log("Game Over! Returning to Main Menu...");
        BackgroundMusicController.Instance.PlayGameOverMusic();
        if (SFXController.Instance != null)
        {
            SFXController.PlayExplosion();
        }
        if (BackgroundMusicController.Instance != null)
        {
            BackgroundMusicController.Instance.PlayGameOverMusic();
        }
        if (!isTest) // 테스트 모드에서는 씬 전환하지 않음
        {
            if (overType == OverBy.Avalanche)
            {
                SceneManager.LoadScene("AvalancheOutro");
            }
            else if (overType == OverBy.Snowball)
            {
                SceneManager.LoadScene("SnowballOutro");
            }
            else if (overType == OverBy.Obstacle)
            {
                SceneManager.LoadScene("AvalancheOutro");
            }
            else
            {
                SceneManager.LoadScene("Outro1");
            }
        }
    }

    public void HandleGameClear(Stats clearStat)
    {
        gameStat.setTime(clearStat.getTime());
        gameStat.setGrade(clearStat.getGrade());
        gameStat.setGradeNum(clearStat.getGradeNum());
        gameStat.setStudent(clearStat.getStudent());
        gameStat.setProfessor(clearStat.getProfessor());
        gameStat.setTotal(clearStat.getTotal());

        this.setState(State.GameClear);
        Debug.Log("Game Clear! Returning to Main Menu...");
        SceneManager.LoadScene("Outro1");
    }

    public void checkStateChange()
    {
        if (gameState != prevState)
        {
            Debug.Log($"State was changed: {prevState} -> {gameState}");
            prevState = gameState;
        }
    }
}

public class Stats
{
    private float grade;
    private int student;
    private float time;
    private int total;
    private int professor;
    private int gradeNum;

    public Stats(float time, float grade, int gradeNum, int student, int professor, int total)
    {
        this.time = time;
        this.grade = grade;
        this.gradeNum = gradeNum;
        this.student = student;
        this.professor = professor;
        this.total = total;
    }

    public float getTime() //time값 반환
    {
        return this.time;
    }

    public void setTime(float time) //time값 설정
    {
        this.time = time;
    }

    public float getGrade() //grade값 반환
    {
        return this.grade;
    }

    public void setGrade(float grade) //grade값 설정
    {
        this.grade = grade;
    }

    public int getGradeNum() //gradeNum값 반환
    {
        return this.gradeNum;
    }

    public void setGradeNum(int gradeNum) //grade값 설정
    {
        this.gradeNum = gradeNum;
    }

    public int getStudent() //student값 반환
    {
        return this.student;
    }

    public void setStudent(int student) //student값 설정
    {
        this.student = student;
    }

    public int getProfessor() //professor값 반환
    {
        return this.professor;
    }

    public void setProfessor(int professor) //professor값 설정
    {
        this.professor = professor;
    }

    public int getTotal() //total값 반환
    {
        return this.total;
    }

    public void setTotal(int total) //total값 설정
    {
        this.total = total;
    }
}
