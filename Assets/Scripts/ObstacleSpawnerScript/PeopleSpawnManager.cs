using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawnManager : MonoBehaviour
{
    public GameObject peoplePrefab; // 사람 프리팹
    public float[] lateralOffsets; // 좌우 오프셋 배열 (플레이어 정면 기준)

    public int maxPeopleToSpawn = 3; // 최대 생성할 사람 수
    private List<float> selectedOffsets = new List<float>(); // 무작위로 선택된 좌우 오프셋
    public Transform playerTransform; // 플레이어 Transform

    private void Awake()
    {
        // 현재 시간을 기반으로 랜덤 시드 초기화
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    // 외부에서 호출 가능한 메서드 (신호 기반)
    public void TriggerSpawn(Transform spawnPoint)
    {
        SelectRandomOffsets();
        StartCoroutine(SpawnPeople(spawnPoint));
    }

    private void SelectRandomOffsets()
    {
        // lateralOffsets 배열에서 3개의 무작위 오프셋 선택
        List<float> availableOffsets = new List<float>(lateralOffsets);

        selectedOffsets.Clear();
        for (int i = 0; i < maxPeopleToSpawn; i++)
        {
            int randomIndex = Random.Range(0, availableOffsets.Count);
            selectedOffsets.Add(availableOffsets[randomIndex]);
            availableOffsets.RemoveAt(randomIndex); // 중복 선택 방지
        }
    }

    private IEnumerator SpawnPeople(Transform spawnPoint)
    {
        for (int i = 0; i < maxPeopleToSpawn; i++)
        {
            if (i >= selectedOffsets.Count)
                break;
            float startOffset = (Random.Range(0, 2) == 0) ? -8 : 6;
            Vector3 startPosition = spawnPoint.position + playerTransform.right * startOffset;
            // 좌우 오프셋 추가 (플레이어 기준)
            Vector3 lateralOffset = playerTransform.right * selectedOffsets[i]; // 수정된 부분
            Vector3 targetPosition = spawnPoint.position + lateralOffset;

            // 사람 방향 설정 (플레이어 반대 방향)
            Quaternion inverseRotation = Quaternion.LookRotation(-playerTransform.forward);

            // 사람 생성
            GameObject newPerson = Instantiate(peoplePrefab, startPosition, inverseRotation);
            Collider carCollider = newPerson.GetComponent<Collider>();

            if (carCollider != null && GameManager.instance.isTest)
            {
                carCollider.isTrigger = true;
            }
            else
            {
                Debug.LogError("Collider 컴포넌트를 찾을 수 없습니다!");
            }

            StartCoroutine(MoveToTarget(newPerson.transform, targetPosition));
            // 약간의 대기 시간 (필요한 경우)
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator MoveToTarget(Transform person, Vector3 targetPosition)
    {
        // person이 null인지 먼저 확인
        if (person == null)
        {
            yield break; // 바로 코루틴 종료
        }

        float speed = 4f; // 이동 속도

        while (person != null && Vector3.Distance(person.position, targetPosition) > 0.1f)
        {
            // person이 아직 존재하는지 매 프레임 확인
            if (person == null)
            {
                yield break; // person이 사라졌다면 즉시 종료
            }

            // 목표 위치로 이동
            person.position = Vector3.MoveTowards(
                person.position,
                targetPosition,
                speed * Time.deltaTime
            );
            yield return null; // 다음 프레임까지 대기
        }
    }
}
