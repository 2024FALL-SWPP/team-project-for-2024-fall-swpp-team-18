using System.Collections;
using UnityEngine;

public class ObjectSpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints; // 스폰 포인트 배열
    public float triggerDistance = 25f; // 플레이어와의 거리 기준
    public CarSpawnManager carSpawnManager; // CarSpawnManager 참조
    public PeopleSpawnManager peopleSpawnManager; // PeopleSpawnManager 참조


    private GameObject player; // Player 오브젝트
    private bool[] isSpawnPointActive; // 각 스폰 포인트 활성화 상태 추적

    private void Start()
    {
        // Player 태그를 가진 오브젝트 찾기
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player 태그를 가진 오브젝트를 찾을 수 없습니다!");
        }

        // 스폰 포인트 활성화 상태 초기화
        isSpawnPointActive = new bool[spawnPoints.Length];
        for (int i = 0; i < isSpawnPointActive.Length; i++)
        {
            isSpawnPointActive[i] = true; // 초기 상태는 모두 활성화
        }
    }
    private void Awake()
        {
            // 현재 시간을 기반으로 랜덤 시드 초기화
            Random.InitState(System.DateTime.Now.Millisecond);
        }



    private void Update()
    {
        if (player == null) return;

        // 각 스폰 포인트를 검사
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!isSpawnPointActive[i]) continue; // 비활성화된 스폰 포인트는 무시

            Transform spawnPoint = spawnPoints[i];
            float distance = Vector3.Distance(player.transform.position, spawnPoint.position);

            // 플레이어가 거리 내에 들어오면 무작위로 스폰 매니저 선택
            if (distance <= triggerDistance)
            {
                TriggerRandomSpawnManager(spawnPoint, i);
            }
        }
    }

    private void TriggerRandomSpawnManager(Transform spawnPoint, int spawnIndex)
    {   Awake();
        isSpawnPointActive[spawnIndex] = false;
        int randomChoice = Random.Range(0, 2); // 0 또는 1 무작위 선택

        if (randomChoice == 0 && carSpawnManager != null)
        {
            // CarSpawnManager 실행
            carSpawnManager.TriggerSpawn(spawnPoint);
        }
        else if (randomChoice == 1 && peopleSpawnManager != null)
        {
            // PeopleSpawnManager 실행
            peopleSpawnManager.TriggerSpawn(spawnPoint);
        }

        // 로그 출력 (디버깅용)
        Debug.Log($"Spawn triggered at {spawnPoint.name} using {(randomChoice == 0 ? "CarSpawnManager" : "PeopleSpawnManager")}");

        // 스폰 포인트 비활성화 시작
  
    }
}
