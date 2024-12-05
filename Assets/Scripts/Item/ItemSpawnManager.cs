using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints; // 스폰 포인트 배열
    public float triggerDistance = 25f; // 플레이어와의 거리 기준
    public GameObject player; // Player 오브젝트
    public GameObject[] itemPrefabs;
    private bool[] isSpawnPointActive; // 각 스폰 포인트 활성화 상태 추적

    void Start()
    {
        isSpawnPointActive = new bool[spawnPoints.Length];
        for (int i = 0; i < isSpawnPointActive.Length; i++)
        {
            isSpawnPointActive[i] = true;
        }
    }

    private void Update()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!isSpawnPointActive[i])
                continue;

            Transform spawnPoint = spawnPoints[i];
            float distance = Vector3.Distance(player.transform.position, spawnPoint.position);

            if (distance <= triggerDistance)
            {
                TriggerRandomSpawnManager(spawnPoint, i);
            }
        }
    }

    private void TriggerRandomSpawnManager(Transform spawnPoint, int spawnIndex)
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        isSpawnPointActive[spawnIndex] = false;
        int randomChoice = Random.Range(0, 10);
        if (randomChoice < 3)
        {
            Instantiate(itemPrefabs[0], spawnPoint.position, spawnPoint.rotation);
            Debug.Log($"Spawn A+");
        }
        else if (randomChoice < 6)
        {
            Instantiate(itemPrefabs[1], spawnPoint.position, spawnPoint.rotation);
            Debug.Log($"Spawn B+");
        }
        else if (randomChoice < 7)
        {
            Instantiate(itemPrefabs[2], spawnPoint.position, spawnPoint.rotation);
            Debug.Log($"Spawn B-");
        }
        else if (randomChoice == 8)
        {
            Instantiate(itemPrefabs[3], spawnPoint.position, spawnPoint.rotation);
            Debug.Log($"Spawn C+");
        }
        else
        {
            Quaternion targetRotation = spawnPoint.rotation * Quaternion.Euler(0, -90, 0);
            Instantiate(itemPrefabs[4], spawnPoint.position, targetRotation);
            Debug.Log($"Spawn F");
        }
    }
}
