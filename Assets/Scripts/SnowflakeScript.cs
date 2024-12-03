using UnityEngine;

public class SnowflakeScript : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {  
        // Player 태그를 가진 객체와 충돌했는지 확인
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            
            // 눈송이 제거
            Destroy(gameObject);
        }
    }
}
