using System.Collections;
using UnityEngine;

public class ChunkSpawnManager : MonoBehaviour
{
    public GameObject[] chunkPrefabs; // 생성할 장애물 프리팹 배열
    public Transform[] spawnPoints; // 여러 스폰 지점 배열
    public float spawnDistance = 10.0f; // 플레이어와의 거리 기준
    public float checkInterval = 0.5f; // 거리 체크 간격

    private GameObject player; // 플레이어 오브젝트

    private void Start()
    {
        // "Player" 태그를 가진 오브젝트 찾기
        player = GameObject.FindGameObjectWithTag("Player");

        // 플레이어가 존재하면 스폰 시작
        if (player != null)
        {
            StartCoroutine(SpawnObstacles());
        }
        else
        {
            Debug.LogError("Player 태그를 가진 오브젝트를 찾을 수 없습니다!");
        }
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                // 플레이어와 각 스폰 지점의 거리 계산
                float distance = Vector3.Distance(player.transform.position, spawnPoint.position);

                // 거리가 spawnDistance 이하인 경우 장애물 생성
                if (distance <= spawnDistance)
                {
                    // 무작위 장애물 선택
                    GameObject randomObstacle = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

                    // 장애물 생성
                    Instantiate(randomObstacle, spawnPoint.position, spawnPoint.rotation);

                    // 중복 생성 방지 (한 번 생성 후 대기)
                    yield return new WaitForSeconds(1.0f);
                }
            }

            // 거리 체크 간격 대기
            yield return new WaitForSeconds(checkInterval);
        }
    }
}
