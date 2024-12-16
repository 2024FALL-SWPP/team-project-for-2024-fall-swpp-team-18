using UnityEngine;
using UnityEngine.AI;

public class ProfessorMove : MonoBehaviour
{
    [SerializeField]
    public GameObject scoreManager;

    [SerializeField]
    public ScoreManager scoreManagerScript;

    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        Vector3 randomPosition = new Vector3(Random.Range(-160, 210), 15f, Random.Range(-6, -3));

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.MovePosition(randomPosition); // Rigidbody를 이용해 위치 이동
        }
    }
}
