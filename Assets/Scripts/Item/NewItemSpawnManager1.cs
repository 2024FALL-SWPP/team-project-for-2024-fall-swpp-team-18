using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class NewItemSpawnManager1 : MonoBehaviour
{
    public float triggerDistance = 25f;
    public GameObject player;
    public Transform[] spawnPoints1;
    public Transform[] spawnPoints2;
    public Transform[] spawnPoints3;
    public GameObject[] scoreItemPrefabs;
    public GameObject[] studentItemPrefabs;
    public GameObject[] ObstacleItemPrefabs;
    public Transform[][] spawnPoints = new Transform[3][];
    private GameObject[][] itemPrefabs = new GameObject[3][];
    private bool[][] isSpawnPointActive = new bool[3][];
    private int[][] chunks = new int[][]
    {
        new int[] { 0, 0, 0 },
        new int[] { 0, 0, 2 },
        new int[] { 0, 0, 1 },
        new int[] { 0, 1, 2 },
        new int[] { 0, 0 },
        new int[] { 0, 2 },
    };
    private List<GameObject> selected = new List<GameObject>();
    int count = 0;

    void Start()
    {
        spawnPoints[0] = spawnPoints1;
        spawnPoints[1] = spawnPoints2;
        spawnPoints[2] = spawnPoints3;
        itemPrefabs[0] = scoreItemPrefabs;
        itemPrefabs[1] = studentItemPrefabs;
        itemPrefabs[2] = ObstacleItemPrefabs;
        for (int j = 0; j < spawnPoints.Length; j++)
        {
            isSpawnPointActive[j] = new bool[spawnPoints[j].Length];
            for (int i = 0; i < isSpawnPointActive[j].Length; i++)
            {
                isSpawnPointActive[j][i] = true;
            }
        }
    }

    private void Update()
    {
        if (count > 12)
            return;
        int j = count < 6 ? count / 3 : 2;
        int i = count < 6 ? count % 3 : count - 6;

        Transform spawnPoint = spawnPoints[j][i];
        float distance = Vector3.Distance(player.transform.position, spawnPoint.position);

        if (distance <= triggerDistance & isSpawnPointActive[j][i])
        {
            count++;
            isSpawnPointActive[j][i] = false;

            Random.InitState(System.DateTime.Now.Millisecond);
            int[] chunk = chunks[Random.Range(0, 5)];

            Vector3 spawnPosition = spawnPoint.position;
            Quaternion spawnRotation = spawnPoint.rotation;
            List<Vector3> spawnPositions = FixSpawnPoint(spawnPosition, j, chunk.Length);

            for (int k = 0; k < chunk.Length; k++)
            {
                Random.InitState(System.DateTime.Now.Millisecond + k * k);
                GameObject item = itemPrefabs[chunk[k]][
                    Random.Range(0, itemPrefabs[chunk[k]].Length)
                ];
                Destroy(Instantiate(item, spawnPositions[k], spawnRotation), 5f);
                Debug.Log($"Spawn triggered at {spawnPoint.name}");
            }
        }
    }

    private List<Vector3> FixSpawnPoint(Vector3 spawnPosition, int stage, int num)
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        int randomChoice = Random.Range(0, 3);
        List<Vector3> fixedSpawnPositions = new List<Vector3>();
        Vector3 fixedSpawnPosition;
        for (int i = randomChoice; i < randomChoice + num; i++)
        {
            int ii = i % 3;
            if (stage == 0)
            {
                fixedSpawnPosition = spawnPosition - new Vector3(2 * ii + 1, 0, 0);
            }
            else if (stage == 1)
            {
                fixedSpawnPosition = spawnPosition - new Vector3(0, 0, 2 * ii + 1);
            }
            else
            {
                fixedSpawnPosition = spawnPosition - new Vector3(2 * ii, 0, 0);
            }
            fixedSpawnPositions.Add(fixedSpawnPosition);
        }
        return fixedSpawnPositions;
    }
}
