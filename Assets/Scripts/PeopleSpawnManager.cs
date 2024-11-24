using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PeopleSpawnManager : MonoBehaviour
{
    public GameObject peoplePrefab; // 사람 프리팹
    public float[] lateralOffsets; // 좌우 오프셋 배열 (플레이어 정면 기준)

    public int maxPeopleToSpawn = 3; // 최대 생성할 사람 수
    private List<float> selectedOffsets = new List<float>(); // 무작위로 선택된 좌우 오프셋
    public Transform playerTransform; // 플레이어 Transform

    private void Start()
    {
        // 좌우 오프셋 무작위 선택
        SelectRandomOffsets();
    }

    private void SelectRandomOffsets()
    {
        // lateralOffsets 배열에서 3개의 무작위 오프셋 선택
        List<float> availableOffsets = new List<float>(lateralOffsets);

        for (int i = 0; i < maxPeopleToSpawn; i++)
        {
            int randomIndex = Random.Range(0, availableOffsets.Count);
            selectedOffsets.Add(availableOffsets[randomIndex]);
            availableOffsets.RemoveAt(randomIndex); // 중복 선택 방지
        }
    }

    // 외부에서 호출 가능한 메서드 (신호 기반)
    public void TriggerSpawn(Transform spawnPoint)
    {
        StartCoroutine(SpawnPeople(spawnPoint));
    }

    private IEnumerator SpawnPeople(Transform spawnPoint)
    {
        for (int i = 0; i < maxPeopleToSpawn; i++)
        {
            if (i >= selectedOffsets.Count) break;


            // 좌우 오프셋 추가 (플레이어 기준)
            Vector3 lateralOffset = playerTransform.right * selectedOffsets[i]; // 수정된 부분
            Vector3 spawnPosition = spawnPoint.position + lateralOffset;

            // 사람 방향 설정 (플레이어 반대 방향)
            Quaternion inverseRotation = Quaternion.LookRotation(-playerTransform.forward);

            // 사람 생성
            Instantiate(peoplePrefab, spawnPosition, inverseRotation);

            // 약간의 대기 시간 (필요한 경우)
            yield return new WaitForSeconds(0.1f);
        }
    }
}
