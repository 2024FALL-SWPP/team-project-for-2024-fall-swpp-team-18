using UnityEngine;

public class TreeFall : MonoBehaviour
{
    public float force = 200000f; // 힘의 크기
    public Vector3 localForceDirection = Vector3.left; // 로컬 기준 힘의 방향
    public GameObject player; // Player 객체
    private Rigidbody rb;
    public float lifetime = 10f;

    void Start()
    {
        // Rigidbody 가져오기
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing!");
        }

        // Player 객체 확인
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("Player object is not assigned or not found in the scene!");
                return;
            }
        }

       

        

        PushTree();

        // 일정 시간 후 나무 삭제
        Destroy(gameObject, lifetime);
    }

    public void PushTree()
    {
        if (rb != null && player != null)
        {   

            // Player의 Transform 기준으로 로컬 방향을 월드 방향으로 변환
            Vector3 worldForceDirection = player.transform.TransformDirection(localForceDirection);
            // 지정된 방향으로 힘 가하기
            rb.AddForce(worldForceDirection * force);
        }
    }
}
