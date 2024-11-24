using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float minSpeed = -3f; // 최소 이동 속도
    public float maxSpeed = 5f; // 최대 이동 속도
    public float lifetime = 10f; // 자동차가 자동으로 삭제되기까지의 시간

    private float speed; // 자동차의 이동 속도

    private void Awake()
    {
        // 현재 시간을 기반으로 랜덤 시드 초기화
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    private void Start()
    {   
        // 랜덤 속도 설정
        speed = Random.Range(minSpeed, maxSpeed);

        // 일정 시간이 지나면 자동차를 제거
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // 자동차를 앞으로 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
