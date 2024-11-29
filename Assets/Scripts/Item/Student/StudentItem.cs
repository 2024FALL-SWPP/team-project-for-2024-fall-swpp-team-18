using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class StudentItem : MonoBehaviour
{
    public GameObject scoreManager;
    public ScoreManager scoreManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update() { }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManagerScript.IncreaseStudent();
        }
    }
}
