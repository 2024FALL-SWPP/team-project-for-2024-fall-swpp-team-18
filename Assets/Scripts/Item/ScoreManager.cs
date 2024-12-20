using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public int heart = 3;

    [SerializeField]
    public int fireball = 0;
    public Stats curStat;

    // Start is called before the first frame update
    void Start()
    {
        curStat = new Stats(0, 0, 0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.getState() == State.Play)
        {
            float playTime = curStat.getTime() + Time.deltaTime;
            curStat.setTime(playTime);
        }
    }

    public void IncreaseGrade(float itemGrade)
    {
        float grade = curStat.getGrade();
        int gradeNum = curStat.getGradeNum();

        grade = (grade * gradeNum + itemGrade) / (gradeNum + 1);
        grade = Mathf.Round(grade * 100) / 100f;

        curStat.setGrade(grade);
        curStat.setGradeNum(gradeNum++);
    }

    public void IncreaseStudent()
    {
        int student = curStat.getStudent();
        curStat.setStudent(student++);
    }

    public void DecreaseHeart()
    {
        heart--;
        if (heart == 0)
        {
            GameManager.instance.HandleGameOver(OverBy.Obstacle, curStat);
        }
    }

    public void IncreaseProfessor()
    {
        int professor = curStat.getProfessor();
        curStat.setProfessor(professor++);
    }

    public void IncreaseHeart()
    {
        heart = Mathf.Min(5, heart + 1);
    }

    public void IncreaseFireball()
    {
        fireball++;
    }

    public int CalculateTotal()
    {
        float playTime = curStat.getTime();
        float grade = curStat.getGrade();
        int gradeNum = curStat.getGradeNum();
        int student = curStat.getStudent();
        int professor = curStat.getProfessor();
        int total = curStat.getTotal();

        if (GameManager.instance.getState() == State.GameClear)
        {
            total = (total + (int)((200 - playTime) * 500)) * (professor + 1);
        }
        else
        {
            total = (int)(grade * 100 * gradeNum) + student * 100 + (int)(playTime * 50);
        }
        curStat.setTotal(total);
        return total;
    }

    public void collideSnowball()
    {
        if (heart == 1)
        {
            GameManager.instance.HandleGameOver(OverBy.Snowball, curStat);
        }
        else
        {
            heart--;
        }
    }

    public void collideAvalanche()
    {
        GameManager.instance.HandleGameOver(OverBy.Avalanche, curStat);
    }

    public void arriveMainGate()
    {
        GameManager.instance.setState(State.GameClear);
        CalculateTotal();
        GameManager.Instance.HandleGameClear(curStat);
    }
}
