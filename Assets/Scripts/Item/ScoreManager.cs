using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public int score = 0;

    [SerializeField]
    public int student = 0;

    [SerializeField]
    public int professor = 0;

    [SerializeField]
    public int heart = 3;

    [SerializeField]
    public int fireball = 0;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (heart == 0)
        {
            //Debug.Log("gameover");
            //GameManager.Instance.GameOver = true;
        }
    }

    public void IncreaseScore(int itemScore)
    {
        score += itemScore;
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
}
