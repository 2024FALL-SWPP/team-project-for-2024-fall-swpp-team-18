using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public float score = 0;

    [SerializeField]
    public int scoreNum = 0;

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
        playTime += Time.deltaTime;
        total = (int)(score * 100) + student * 100 + scoreNum * 100;
        if (heart == 0)
        {
            Debug.Log("gameover");
            GameManager.Instance.HandleGameOver();
        }
    }

    public void IncreaseScore(int itemScore)
    {
        score = (score * scoreNum + itemScore) / (scoreNum + 1);
        score = Mathf.Round(score * 100) / 100f;
        scoreNum++;
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
        heart++;
    }

    public void IncreaseFireball()
    {
        fireball++;
    }

    public void CalculateTotal()
    {
        score = (score + (200 - playTime) * 500) * (professor + 1);
    }
}
