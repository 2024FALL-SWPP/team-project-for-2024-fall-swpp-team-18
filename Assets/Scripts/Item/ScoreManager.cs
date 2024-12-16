using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public float grade = 0;

    [SerializeField]
    public int gradeNum = 0;

    [SerializeField]
    public int total = 0;

    [SerializeField]
    public int student = 0;

    [SerializeField]
    public int professor = 0;

    [SerializeField]
    public int heart = 3;

    [SerializeField]
    public int fireball = 0;

    [SerializeField]
    public float playTime;

    // Start is called before the first frame update
    void Start()
    {
        playTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            playTime += Time.deltaTime;
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

    public void IncreaseProfessor()
    {
        professor++;
    }

    public void IncreaseHeart()
    {
        heart = Mathf.Min(heart + 1, 3);

    }

    public void IncreaseFireball()
    {
        fireball++;
    }

    public int CalculateTotal()
    {
        return ((int)(grade * 100 * gradeNum) + student * 100 + (int)((200 - playTime) * 500))
            * (professor + 1);
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
        total = CalculateTotal();
        GameManager.Instance.HandleGameClear(grade, student, playTime, total, professor, gradeNum);
    }
}
