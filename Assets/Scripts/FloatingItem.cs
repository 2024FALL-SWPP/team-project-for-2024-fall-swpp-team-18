using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    // 떠오르는 속도
    public float floatSpeed = 2f;

    // 떠오르는 높이 범위
    public float floatHeight = 0.5f;
    private Vector3 initialPosition;

    void Start()
    {
        // 초기 위치 저장
        initialPosition = transform.position;
    }

    void Update()
    {
        // 상하로 움직임
        float newY = initialPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}
