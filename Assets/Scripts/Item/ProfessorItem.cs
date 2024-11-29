using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ProfessorItem : MonoBehaviour
{
    public GameObject scoreManager;
    private ScoreManager scoreManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManagerScript.IncreaseProfessor();
            Destroy(gameObject);
        }
    }
}
