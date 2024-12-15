using System.Collections;
using UnityEngine;

public class SnowballSpawnManager : MonoBehaviour
{
    public GameObject snowballPrefab; // 스폰할 눈덩이 프리팹
    public Transform playerTransform; // 플레이어 Transform
    public Vector3[] spawnPositions; // 눈덩이 생성 위치 배열
    private float snowballSpeed = 20.0f; // 눈덩이의 이동 속도
    private float snowballLifetime = 10.0f; // 눈덩이 생존 시간
    private float spawnInterval = 5.0f; // 눈덩이 생성 주기

    private void Start()
    {
        if (spawnPositions.Length == 0)
        {
            Debug.LogError("No spawn positions assigned!");
            return;
        }

        // 5초마다 눈덩이를 스폰
        StartCoroutine(SpawnSnowballs());
    }

    private IEnumerator SpawnSnowballs()
    {
        while (true)
        {
            foreach (Vector3 spawnPosition in spawnPositions)
            {
                SpawnSnowball(spawnPosition);
                yield return new WaitForSeconds(spawnInterval); // 각 위치마다 spawnInterval만큼 대기
            }
        }
    }

    private void SpawnSnowball(Vector3 spawnPosition)
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned!");
            return;
        }

        // 눈덩이 생성
        GameObject snowball = Instantiate(snowballPrefab, spawnPosition, Quaternion.identity);

        // 눈덩이의 움직임 처리
        SnowballController snowballController = snowball.GetComponent<SnowballController>();
        if (snowballController != null)
        {
            snowballController.SetTarget(playerTransform, snowballSpeed, snowballLifetime);
        }
        else
        {
            Debug.LogError("Snowball prefab is missing SnowballController script!");
        }
    }
}
