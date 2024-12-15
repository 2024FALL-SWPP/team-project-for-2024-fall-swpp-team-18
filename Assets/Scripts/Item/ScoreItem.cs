using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    public float scoreValue;

    private GameObject scoreManager;
    private ScoreManager scoreManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager");
        scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManagerScript.IncreaseGrade(scoreValue);
            Destroy(gameObject);
        }
    }
}
