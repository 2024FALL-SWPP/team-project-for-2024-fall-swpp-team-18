using System.Collections;
using UnityEngine;

public class SnowballSpawnManager : MonoBehaviour
{
    public GameObject player; // Player 객체
    private Transform playerTransform; // 플레이어 Transform

    public GameObject[] snowballPrefab; // 스폰할 눈덩이 프리팹
    public Transform[] spawnPoints; // 눈덩이 생성 위치 오브젝트들
    private float snowballSpeed = 20.0f;
    private float snowballLifetime = 20.0f;
    public float spawnInterval;

    private void Start()
    {
        spawnInterval = GameManager.instance.isEasy ? 10.0f : 5.0f;
        if (player == null)
        {
            Debug.LogError("Player object is not assigned!");
            return;
        }

        playerTransform = player.transform;

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        StartCoroutine(SpawnSnowballs());
    }

    private IEnumerator SpawnSnowballs()
    {
        yield return new WaitForSeconds(spawnInterval);
        while (true)
        {
            // spawnPoints 배열을 돌면서 각 위치에서 눈덩이 스폰
            foreach (Transform spawnPoint in spawnPoints)
            {
                SpawnSnowball(spawnPoint);
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    private void SpawnSnowball(Transform spawnPoint)
    {
        PlayerPositionController playerController = player.GetComponent<PlayerPositionController>();
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned!");
            return;
        }

        // 현재 스테이지에 따라 다른 스폰 포인트 선택
        int stageIndex = playerController.getStage() - 1;
        if (stageIndex < 0 || stageIndex >= spawnPoints.Length)
        {
            Debug.LogError("Invalid stage index for spawnPoints!");
            return;
        }

        Transform selectedSpawnPoint = spawnPoints[stageIndex];
        // 눈덩이 생성
        Random.InitState(System.DateTime.Now.Millisecond);
        GameObject snowball = Instantiate(
            snowballPrefab[stageIndex],
            selectedSpawnPoint.position + new Vector3(Random.Range(-3, 4), 0, 0),
            Quaternion.identity
        );
        Collider carCollider = snowball.GetComponent<Collider>();

        if (carCollider != null && GameManager.instance.isTest)
        {
            carCollider.isTrigger = true;
        }
        else
        {
            Debug.LogError("Collider 컴포넌트를 찾을 수 없습니다!");
        }

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
