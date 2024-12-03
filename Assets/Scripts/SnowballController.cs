using UnityEngine;

public class SnowballController : MonoBehaviour
{
    private Transform playerTransform; // 플레이어 Transform
    private float speed; // 눈덩이의 이동 속도
    private float lifetime; // 눈덩이 생존 시간
    private Vector3 direction;
    public void SetTarget(Transform target, float moveSpeed, float lifeTime)
    {
        playerTransform = target;
        speed = moveSpeed;
        lifetime = lifeTime;

        // 눈덩이 자동 제거
        Destroy(gameObject, lifetime);
    }
    private void Start(){
        if (playerTransform != null)
        {
            // 플레이어를 향해 이동
            direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            // 플레이어를 향해 이동
            transform.position += direction * speed * Time.deltaTime;
        }
    }

}
