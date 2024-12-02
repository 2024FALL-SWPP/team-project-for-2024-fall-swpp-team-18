using UnityEngine;

public class HurricaneController : MonoBehaviour
{
    public float moveSpeed = 5f; // 허리케인의 이동 속도
    public float rotationSpeed = 300f; // 허리케인의 회전 속도

    private Rigidbody rb; // Rigidbody 컴포넌트
    private Transform playerTransform; // 플레이어의 Transform

    void Start()
    {
        // Rigidbody 가져오기
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing!");
        }

        // Scene에서 Player 태그를 가진 객체 찾기
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found in the scene!");
        }

    }

    void FixedUpdate()
    {
        // 플레이어를 향해 이동
        MoveTowardsPlayer();

        // 허리케인의 회전
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    private void MoveTowardsPlayer()
{
    if (playerTransform == null)
    {
        Debug.LogWarning("Player Transform is not assigned.");
        return;
    }

    // 플레이어 방향 계산
    Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

    // 경사면을 따라 이동 (중력 포함)
    Vector3 targetVelocity = directionToPlayer * moveSpeed;
    rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);

    // 경사면을 따라 정확한 Y 위치를 보정 (필요 시 추가)
    RaycastHit hit;
    if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 2f))
    {
        transform.position = new Vector3(transform.position.x, hit.point.y+ 1f, transform.position.z);
    }
}

}
