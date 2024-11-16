using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fireball")) // Fireball 태그를 가진 오브젝트와 충돌 시
        {
            Destroy(gameObject); // 현재 장애물 오브젝트를 제거합니다.
        }
    }
}
