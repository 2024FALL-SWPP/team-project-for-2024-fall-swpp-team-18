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
    void Start() { }

    public void TriggerSpawn13(Transform spawnPoint)
    {
        StartCoroutine(SpawnJump(spawnPoint, spawnjumpOffsets13));
        StartCoroutine(SpawnLongTree(spawnPoint, spawnOffsets13));
    }

    public void TriggerSpawn2(Transform spawnPoint)
    {
        StartCoroutine(SpawnJump(spawnPoint, spawnjumpOffsets2));
        StartCoroutine(SpawnLongTree(spawnPoint, spawnOffsets2));
    }

    private IEnumerator SpawnJump(Transform spawnPoint, Vector3[] spawnjumpOffsets)
    {
        int chosenint = Random.Range(0, 2);

        Vector3 worldjumpOffset =
            playerTransform.TransformPoint(spawnjumpOffsets[chosenint])
            - playerTransform.position;

        Vector3 spawnjumpPosition = spawnPoint.position + worldjumpOffset;

        Quaternion jumpRotation = Quaternion.LookRotation(playerTransform.forward);

        Instantiate(jumpObject, spawnjumpPosition, jumpRotation);

        // 생성 간격 (필요 시 수정)
        yield return new WaitForSeconds(0.25f);
    }

    

    private IEnumerator SpawnLongTree(Transform spawnPoint, Vector3[] spawnOffsets)
{
    int chosenIndex = Random.Range(0, 2);
    int unchosenIndex = 1 - chosenIndex;
    List<GameObject> availableTrees = new List<GameObject>(treePrefabs);

    // 나무 생성 위치와 순서를 정의
    int[] spawnOrder = { chosenIndex, unchosenIndex, chosenIndex, unchosenIndex };
    int[] offsetIndices = { 0, 2, 4, 6 };

    for (int i = 0; i < spawnOrder.Length; i++)
    {
        int treeIndex = spawnOrder[i];
        int offsetIndex = offsetIndices[i];

        // 스폰 위치 및 방향 계산
        Vector3 worldOffset = CalculateWorldOffset(spawnOffsets[treeIndex + offsetIndex]);
        Vector3 spawnPosition = spawnPoint.position + worldOffset;
        Quaternion treeRotation = Quaternion.LookRotation(playerTransform.forward);

        // 나무 생성 및 충돌체 설정
        SpawnTree(availableTrees[treeIndex], spawnPosition, treeRotation);

        // 생성 간격 (필요 시 조정 가능)
        yield return new WaitForSeconds(0.3f);
    }
}

private Vector3 CalculateWorldOffset(Vector3 localOffset)
{
    return playerTransform.TransformPoint(localOffset) - playerTransform.position;
}

private void SpawnTree(GameObject treePrefab, Vector3 position, Quaternion rotation)
{
    GameObject tree = Instantiate(treePrefab, position, rotation);
    Collider treeCollider = tree.GetComponent<Collider>();

    if (treeCollider != null && GameManager.instance.isTest)
    {
        treeCollider.isTrigger = true;
    }
    else if (treeCollider == null)
    {
        Debug.LogError("Collider 컴포넌트를 찾을 수 없습니다!");
    }
}


    // Update is called once per frame
    void Update() { }
}
