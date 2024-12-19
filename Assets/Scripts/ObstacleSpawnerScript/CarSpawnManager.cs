using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnManager : MonoBehaviour
{
    public GameObject[] carPrefabs; // 자동차 프리팹 배열 (4종류)
    public Vector3[] spawnOffsets; // 스폰 포인트별 오프셋 배열 (플레이어 기준)
    public Transform playerTransform; // 플레이어 Transform
    private List<GameObject> selectedCars = new List<GameObject>(); // 무작위로 선택된 자동차 저장
    public int maxCarsToSpawn = 3; // 최대 생성할 자동차 수

    private void Awake()
    {
        // 현재 시간을 기반으로 랜덤 시드 초기화
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    private void Start()
    {
        // 자동차 3종류 무작위 선택
        SelectRandomCars();
    }

    private void SelectRandomCars()
    {
        // 4개의 자동차 중 3종류를 무작위로 선택
        List<GameObject> availableCars = new List<GameObject>(carPrefabs);

        for (int i = 0; i < maxCarsToSpawn; i++)
        {
            int randomIndex = Random.Range(0, availableCars.Count);
            selectedCars.Add(availableCars[randomIndex]);
            availableCars.RemoveAt(randomIndex); // 중복 선택 방지
        }
    }

    // 외부에서 호출 가능한 메서드 (신호 기반으로 호출)
    public void TriggerSpawn(Transform spawnPoint)
    {
        SelectRandomCars();
        StartCoroutine(SpawnCars(spawnPoint));
    }

    private IEnumerator SpawnCars(Transform spawnPoint)
    {
        Vector3 basePosition =
            (Random.Range(0, 2) == 0) ? new Vector3(0, 0, 0) : new Vector3(2, 0, 0);

        for (int i = 0; i < maxCarsToSpawn; i++)
        {
            if (i >= spawnOffsets.Length)
                break;

            // 플레이어 기준 spawnOffsets[i]를 월드 좌표로 변환
            Vector3 worldOffset =
                playerTransform.TransformPoint(spawnOffsets[i]) - playerTransform.position;

            // 최종 스폰 위치 계산
            Vector3 spawnPosition = spawnPoint.position + worldOffset + basePosition;

            // 자동차 방향 설정 (플레이어의 정면 방향)
            Quaternion carRotation = Quaternion.LookRotation(playerTransform.forward);

            // 무작위로 선택된 자동차 중 하나를 생성
            GameObject randomCar = selectedCars[i];
            GameObject newCar = Instantiate(randomCar, spawnPosition, carRotation);
            Collider carCollider = newCar.GetComponent<Collider>();

            if (carCollider != null && GameManager.instance.isTest)
            {
                carCollider.isTrigger = true;
            }
            else
            {
                Debug.LogError("Collider 컴포넌트를 찾을 수 없습니다!");
            }

            // 생성 간격 (필요 시 수정)
            yield return new WaitForSeconds(0.1f);
        }
    }
}
