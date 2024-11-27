using UnityEngine;

public class TreeFall : MonoBehaviour
{
    public float force = 500f; // 힘의 크기
    public Vector3 forceDirection = Vector3.left; // 힘의 방향

    private Rigidbody rb;

    void Start()
    {
        // Rigidbody 가져오기
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing!");
        }

        PushTree();
    }

    public void PushTree()
    {
        if (rb != null)
        {
            // 지정된 방향으로 힘 가하기
            rb.AddForce(forceDirection * force);
        }
    }



}
