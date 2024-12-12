using UnityEngine;
using UnityEngine.AI;

public class ProfessorMove : MonoBehaviour
{
    public float speed = 5f; // 앞으로 이동하는 속도
    public float amplitude = 2f; // 진동의 세기 (양 옆으로 얼마나 움직이는지)
    public float frequency = 1f; // 진동의 속도

    private float time;

    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        transform.localPosition = new Vector3(Random.Range(-170, 220), 65f, -9.5f);
    }

    void Update()
    {
        // 시간 기반으로 앞으로 이동
        time += Time.deltaTime;
        float forwardMovement = speed * Time.deltaTime;

        // 사인 함수를 사용한 좌우 진동 계산
        float sideMovement = Mathf.Sin(time * frequency) * amplitude;

        // 새로운 위치 계산 (예: Z축으로 앞으로 이동, X축으로 좌우 진동)
        Vector3 newPosition = transform.position;
        if (newPosition.x > 210)
            speed = -5;
        else if (newPosition.x < -180)
            speed = 5;
        newPosition.x += forwardMovement; // 앞으로 이동
        newPosition.z = sideMovement - 4; // 좌우 진동

        // 객체의 위치 업데이트
        transform.position = newPosition;
    }
}
