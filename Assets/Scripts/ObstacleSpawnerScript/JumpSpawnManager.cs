using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSpawnManager : MonoBehaviour
{
    public GameObject[] treePrefabs; // 나무 프리팹 배열 (2종류)
    public GameObject jumpObject;
    public Vector3[] spawnjumpOffsets13;
    public Vector3[] spawnOffsets13; // 스폰 포인트별 오프셋 배열 (플레이어 기준)
    public Vector3[] spawnjumpOffsets2;
    public Vector3[] spawnOffsets2; // 스폰 포인트별 오프셋 배열 (플레이어 기준)
    public Transform playerTransform; // 플레이어 Transform
    

    private void Awake()
    {
        // 현재 시간을 기반으로 랜덤 시드 초기화
        Random.InitState(System.DateTime.Now.Millisecond);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TriggerSpawn13(Transform spawnPoint)
    {   
        StartCoroutine(SpawnJump13(spawnPoint));
        StartCoroutine(SpawnLongTree13(spawnPoint));
    }

    public void TriggerSpawn2(Transform spawnPoint)
    {   
        StartCoroutine(SpawnJump2(spawnPoint));
        StartCoroutine(SpawnLongTree2(spawnPoint));
    }

    private IEnumerator SpawnJump13(Transform spawnPoint){

        int chosenint = Random.Range(0, 2);
        
        Vector3 worldjumpOffset = playerTransform.TransformPoint(spawnjumpOffsets13[chosenint]) - playerTransform.position;

        Vector3 spawnjumpPosition = spawnPoint.position + worldjumpOffset;

        Quaternion jumpRotation = Quaternion.LookRotation(playerTransform.forward);

        Instantiate(jumpObject, spawnjumpPosition, jumpRotation);

        // 생성 간격 (필요 시 수정)
        yield return new WaitForSeconds(0.3f);
    }

    private IEnumerator SpawnLongTree13(Transform spawnPoint)
    {
    int chosenint = Random.Range(0, 2);
    int unchosenint = 1 - chosenint;
    List<GameObject> availableTrees = new List<GameObject>(treePrefabs);

    // 플레이어 기준 spawnOffsets을 월드 좌표로 변환
    Vector3 worldOffset = playerTransform.TransformPoint(spawnOffsets13[chosenint]) - playerTransform.position;

    // 최종 스폰 위치 계산
    Vector3 spawnPosition = spawnPoint.position + worldOffset;

    // 나무 방향 설정 (플레이어를 정면으로 바라보게 하고, 로컬 축 조정)
    Quaternion treeRotation = Quaternion.LookRotation(playerTransform.forward);

    // 무작위로 선택된 나무 중 하나를 생성
    GameObject randomTree = availableTrees[chosenint];
    Instantiate(randomTree, spawnPosition, treeRotation);

    // 생성 간격 (필요 시 수정)
    yield return new WaitForSeconds(0.3f);

    // 플레이어 기준 spawnOffsets을 월드 좌표로 변환
    worldOffset = playerTransform.TransformPoint(spawnOffsets13[unchosenint + 2]) - playerTransform.position;

    // 최종 스폰 위치 계산
    spawnPosition = spawnPoint.position + worldOffset;

    // 무작위로 선택된 나무 중 하나를 생성
    randomTree = availableTrees[unchosenint];
    Instantiate(randomTree, spawnPosition, treeRotation);

    // 생성 간격 (필요 시 수정)
    yield return new WaitForSeconds(0.3f);

    // 플레이어 기준 spawnOffsets을 월드 좌표로 변환
    worldOffset = playerTransform.TransformPoint(spawnOffsets13[chosenint + 4]) - playerTransform.position;

    // 최종 스폰 위치 계산
    spawnPosition = spawnPoint.position + worldOffset;

    // 무작위로 선택된 나무 중 하나를 생성
    randomTree = availableTrees[chosenint];
    Instantiate(randomTree, spawnPosition, treeRotation);

    // 생성 간격 (필요 시 수정)
    yield return new WaitForSeconds(0.3f);

    // 플레이어 기준 spawnOffsets을 월드 좌표로 변환
    worldOffset = playerTransform.TransformPoint(spawnOffsets13[unchosenint + 6]) - playerTransform.position;

    // 최종 스폰 위치 계산
    spawnPosition = spawnPoint.position + worldOffset;

    // 무작위로 선택된 나무 중 하나를 생성
    randomTree = availableTrees[unchosenint];
    Instantiate(randomTree, spawnPosition, treeRotation);

    // 생성 간격 (필요 시 수정)
    yield return new WaitForSeconds(0.3f);
    }

    private IEnumerator SpawnJump2(Transform spawnPoint){

        int chosenint = Random.Range(0, 2);
        
        Vector3 worldjumpOffset = playerTransform.TransformPoint(spawnjumpOffsets2[chosenint]) - playerTransform.position;

        Vector3 spawnjumpPosition = spawnPoint.position + worldjumpOffset;

        Quaternion jumpRotation = Quaternion.LookRotation(playerTransform.forward);

        Instantiate(jumpObject, spawnjumpPosition, jumpRotation);

        // 생성 간격 (필요 시 수정)
        yield return new WaitForSeconds(0.3f);
    }

    private IEnumerator SpawnLongTree2(Transform spawnPoint)
    {
    int chosenint = Random.Range(0, 2);
    int unchosenint = 1 - chosenint;
    List<GameObject> availableTrees = new List<GameObject>(treePrefabs);

    // 플레이어 기준 spawnOffsets을 월드 좌표로 변환
    Vector3 worldOffset = playerTransform.TransformPoint(spawnOffsets2[chosenint]) - playerTransform.position;

    // 최종 스폰 위치 계산
    Vector3 spawnPosition = spawnPoint.position + worldOffset;

    // 나무 방향 설정 (플레이어를 정면으로 바라보게 하고, 로컬 축 조정)
    Quaternion treeRotation = Quaternion.LookRotation(playerTransform.forward);

    // 무작위로 선택된 나무 중 하나를 생성
    GameObject randomTree = availableTrees[chosenint];
    Instantiate(randomTree, spawnPosition, treeRotation);

    // 생성 간격 (필요 시 수정)
    yield return new WaitForSeconds(0.3f);

    // 플레이어 기준 spawnOffsets을 월드 좌표로 변환
    worldOffset = playerTransform.TransformPoint(spawnOffsets2[unchosenint + 2]) - playerTransform.position;

    // 최종 스폰 위치 계산
    spawnPosition = spawnPoint.position + worldOffset;

    // 무작위로 선택된 나무 중 하나를 생성
    randomTree = availableTrees[unchosenint];
    Instantiate(randomTree, spawnPosition, treeRotation);

    // 생성 간격 (필요 시 수정)
    yield return new WaitForSeconds(0.3f);

    // 플레이어 기준 spawnOffsets을 월드 좌표로 변환
    worldOffset = playerTransform.TransformPoint(spawnOffsets2[chosenint + 4]) - playerTransform.position;

    // 최종 스폰 위치 계산
    spawnPosition = spawnPoint.position + worldOffset;

    // 무작위로 선택된 나무 중 하나를 생성
    randomTree = availableTrees[chosenint];
    Instantiate(randomTree, spawnPosition, treeRotation);

    // 생성 간격 (필요 시 수정)
    yield return new WaitForSeconds(0.3f);

    // 플레이어 기준 spawnOffsets을 월드 좌표로 변환
    worldOffset = playerTransform.TransformPoint(spawnOffsets2[unchosenint + 6]) - playerTransform.position;

    // 최종 스폰 위치 계산
    spawnPosition = spawnPoint.position + worldOffset;

    // 무작위로 선택된 나무 중 하나를 생성
    randomTree = availableTrees[unchosenint];
    Instantiate(randomTree, spawnPosition, treeRotation);

    // 생성 간격 (필요 시 수정)
    yield return new WaitForSeconds(0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
