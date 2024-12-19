using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public float grade;

    [SerializeField]
    public int gradeNum;

    [SerializeField]
    public int total;

    [SerializeField]
    public int student;

    [SerializeField]
    public int professor;

    [SerializeField]
    public int heart;

    [SerializeField]
    public int fireball;

    [SerializeField]
    public float playTime;

    // Start is called before the first frame update
    void Start()
    {
        playTime = 0f;
        grade = 0;
        gradeNum = 0;
        student = 0;
        professor = 0;
        total = 0;
        fireball = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            playTime += Time.deltaTime;
        }
    }

    public void IncreaseGrade(float itemGrade)
    {
        grade = (grade * gradeNum + itemGrade) / (gradeNum + 1);
        grade = Mathf.Round(grade * 100) / 100f;
        gradeNum++;
    }

    public void IncreaseStudent()
    {
        student++;
    }

    public void DecreaseHeart()
    {
        heart--;
        if (heart == 0)
        {
            GameManager.Instance.HandleGameOver(
                GameManager.OverBy.Obstacle,
                grade,
                student,
                playTime,
                total,
                professor,
                gradeNum
            );
        }
    }

    public void IncreaseProfessor()
    {
        professor++;
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
        if (GameManager.instance.isGameClear)
        {
            total = (total + (int)((200 - playTime) * 500)) * (professor + 1);
        }
        else
        {
            total = (int)(grade * 100 * gradeNum) + student * 100 + (int)(playTime * 50);
        }
        return total;
    }

    public void collideSnowball()
    {
        if (heart == 1)
        {
            GameManager.instance.HandleGameOver(
                GameManager.OverBy.Snowball,
                grade,
                student,
                playTime,
                total,
                professor,
                gradeNum
            );
        }
        else
        {
            heart--;
        }
    }

    public void collideAvalanche()
    {
        GameManager.instance.HandleGameOver(
            GameManager.OverBy.Avalanche,
            grade,
            student,
            playTime,
            total,
            professor,
            gradeNum
        );
    }

    public void arriveMainGate()
    {
        GameManager.instance.isGameClear = true;
        total = CalculateTotal();
        GameManager.Instance.HandleGameClear(grade, student, playTime, total, professor, gradeNum);
    }
}
