using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawnManager : MonoBehaviour
{
    public GameObject[] treePrefabs; // 나무 프리팹 배열 (2종류)
    public Vector3[] spawnOffsets13; // 스폰 포인트별 오프셋 배열 (플레이어 기준)
    public Vector3[] spawnOffsets2; // 스폰 포인트별 오프셋 배열 (플레이어 기준)
    public Transform playerTransform; // 플레이어 Transform

    public int maxtreesToSpawn = 3; // 최대 생성할 나무 수

    private void Awake()
    {
        // 현재 시간을 기반으로 랜덤 시드 초기화
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    // Start is called before the first frame update
    void Start() { }

    public void TriggerSpawn13(Transform spawnPoint)
    {
        StartCoroutine(SpawnTrees13(spawnPoint));
    }

    public void TriggerSpawn2(Transform spawnPoint)
    {
        StartCoroutine(SpawnTrees2(spawnPoint));
    }

    private IEnumerator SpawnTrees13(Transform spawnPoint)
    {
        int chosenint = Random.Range(0, 2);
        int unchosenint = 1 - chosenint;
        List<GameObject> availableTrees = new List<GameObject>(treePrefabs);

        // 플레이어 기준 spawnOffsets을 월드 좌표로 변환
        Vector3 worldOffset =
            playerTransform.TransformPoint(spawnOffsets13[chosenint]) - playerTransform.position;

        // 최종 스폰 위치 계산
        Vector3 spawnPosition = spawnPoint.position + worldOffset;

        // 나무 방향 설정 (플레이어를 정면으로 바라보게 하고, 로컬 축 조정)
        Quaternion treeRotation = Quaternion.LookRotation(playerTransform.forward);

        // 무작위로 선택된 나무 중 하나를 생성
        GameObject randomTree = availableTrees[chosenint];
        GameObject newTree1 = Instantiate(randomTree, spawnPosition, treeRotation);
        Collider carCollider = newTree1.GetComponent<Collider>();

        if (carCollider != null && GameManager.instance.isTest)
        {
            carCollider.isTrigger = true;
        }
        else
        {
            Debug.LogError("Collider 컴포넌트를 찾을 수 없습니다!");
        }

        // 생성 간격 (필요 시 수정)
        yield return new WaitForSeconds(0.3f);

        // 플레이어 기준 spawnOffsets을 월드 좌표로 변환
        worldOffset =
            playerTransform.TransformPoint(spawnOffsets13[unchosenint + 2])
            - playerTransform.position;

        // 최종 스폰 위치 계산
        spawnPosition = spawnPoint.position + worldOffset;

        // 무작위로 선택된 나무 중 하나를 생성
        randomTree = availableTrees[unchosenint];
        GameObject newTree2 = Instantiate(randomTree, spawnPosition, treeRotation);
        Collider carCollider2 = newTree2.GetComponent<Collider>();

        if (carCollider2 != null && GameManager.instance.isTest)
        {
            carCollider2.isTrigger = true;
        }
        else
        {
            Debug.LogError("Collider 컴포넌트를 찾을 수 없습니다!");
        }

        // 생성 간격 (필요 시 수정)
        yield return new WaitForSeconds(0.3f);

        // 플레이어 기준 spawnOffsets을 월드 좌표로 변환
        worldOffset =
            playerTransform.TransformPoint(spawnOffsets13[chosenint + 4])
            - playerTransform.position;

        // 최종 스폰 위치 계산
        spawnPosition = spawnPoint.position + worldOffset;

        // 무작위로 선택된 나무 중 하나를 생성
        randomTree = availableTrees[chosenint];
        GameObject newTree3 = Instantiate(randomTree, spawnPosition, treeRotation);
        Collider carCollider3 = newTree3.GetComponent<Collider>();

        if (carCollider3 != null && GameManager.instance.isTest)
        {
            carCollider3.isTrigger = true;
        }
        else
        {
            Debug.LogError("Collider 컴포넌트를 찾을 수 없습니다!");
        }

        // 생성 간격 (필요 시 수정)
        yield return new WaitForSeconds(0.3f);
    }

    private IEnumerator SpawnTrees2(Transform spawnPoint)
    {
        int chosenint = Random.Range(0, 2);
        int unchosenint = 1 - chosenint;
        List<GameObject> availableTrees = new List<GameObject>(treePrefabs);

        // 플레이어 기준 spawnOffsets을 월드 좌표로 변환
        Vector3 worldOffset =
            playerTransform.TransformPoint(spawnOffsets2[chosenint]) - playerTransform.position;

        // 최종 스폰 위치 계산
        Vector3 spawnPosition = spawnPoint.position + worldOffset;

        // 나무 방향 설정 (플레이어를 정면으로 바라보게 하고, 로컬 축 조정)
        Quaternion treeRotation = Quaternion.LookRotation(playerTransform.forward);

        // 무작위로 선택된 나무 중 하나를 생성
        GameObject randomTree = availableTrees[chosenint];
        Collider carCollider1 = Instantiate(randomTree, spawnPosition, treeRotation)
            .GetComponent<Collider>();

        if (carCollider1 != null && GameManager.instance.isTest)
        {
            carCollider1.isTrigger = true;
        }
        else
        {
            Debug.LogError("Collider 컴포넌트를 찾을 수 없습니다!");
        }

        // 생성 간격 (필요 시 수정)
        yield return new WaitForSeconds(0.3f);

        // 플레이어 기준 spawnOffsets을 월드 좌표로 변환
        worldOffset =
            playerTransform.TransformPoint(spawnOffsets2[unchosenint + 2])
            - playerTransform.position;

        // 최종 스폰 위치 계산
        spawnPosition = spawnPoint.position + worldOffset;

        // 무작위로 선택된 나무 중 하나를 생성
        randomTree = availableTrees[unchosenint];
        Collider carCollider2 = Instantiate(randomTree, spawnPosition, treeRotation)
            .GetComponent<Collider>();

        if (carCollider2 != null && GameManager.instance.isTest)
        {
            carCollider2.isTrigger = true;
        }
        else
        {
            Debug.LogError("Collider 컴포넌트를 찾을 수 없습니다!");
        }

        // 생성 간격 (필요 시 수정)
        yield return new WaitForSeconds(0.3f);

        // 플레이어 기준 spawnOffsets을 월드 좌표로 변환
        worldOffset =
            playerTransform.TransformPoint(spawnOffsets2[chosenint + 4]) - playerTransform.position;

        // 최종 스폰 위치 계산
        spawnPosition = spawnPoint.position + worldOffset;

        // 무작위로 선택된 나무 중 하나를 생성
        randomTree = availableTrees[chosenint];
        Collider carCollider3 = Instantiate(randomTree, spawnPosition, treeRotation)
            .GetComponent<Collider>();

        if (carCollider3 != null && GameManager.instance.isTest)
        {
            carCollider3.isTrigger = true;
        }
        else
        {
            Debug.LogError("Collider 컴포넌트를 찾을 수 없습니다!");
        }

        // 생성 간격 (필요 시 수정)
        yield return new WaitForSeconds(0.3f);
    }

    // Update is called once per frame
    void Update() { }
}
