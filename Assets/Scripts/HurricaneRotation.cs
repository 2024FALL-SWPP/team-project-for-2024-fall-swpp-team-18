using UnityEngine;

public class HurricaneRotation : MonoBehaviour
{
    public float rotationSpeed = 300f; // 회전 속도 (초당 회전 각도)

    void Update()
    {
        // Y축을 기준으로 회전
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
