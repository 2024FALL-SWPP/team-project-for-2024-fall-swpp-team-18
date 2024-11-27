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

        // 경사면을 고려한 위치 보정
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 1f))
        {
            // 경사면 높이에 따라 Y축 위치 보정
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }

        // 플레이어를 향해 이동
        rb.MovePosition(transform.position + directionToPlayer * moveSpeed * Time.deltaTime);
    }
}
