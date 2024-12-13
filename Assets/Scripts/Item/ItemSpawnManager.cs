using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints1;
    public Transform[] spawnPoints2;
    public Transform[] spawnPoints3;
    public float triggerDistance = 25f;
    public GameObject player;
    public GameObject[] itemPrefabs;
    public bool isScore = true;
    public Transform[][] spawnPoints = new Transform[3][];
    private bool[][] isSpawnPointActive = new bool[3][];
    private int prefabNum;

    void Start()
    {
        prefabNum = itemPrefabs.Length;
        spawnPoints[0] = spawnPoints1;
        spawnPoints[1] = spawnPoints2;
        spawnPoints[2] = spawnPoints3;
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
        for (int j = 0; j < spawnPoints.Length; j++)
        {
            for (int i = 0; i < spawnPoints[j].Length; i++)
            {
                if (!isSpawnPointActive[j][i])
                    continue;

                Transform spawnPoint = spawnPoints[j][i];

                List<Vector3> spawnPositions = new List<Vector3>();
                Vector3 spawnPosition = spawnPoint.position;
                Quaternion spawnRotation = spawnPoint.rotation;
                float distance = Vector3.Distance(player.transform.position, spawnPoint.position);

                if (isScore)
                    spawnPositions = FixSpawnPoint(spawnPosition, j);
                else
                    spawnPositions.Add(spawnPoint.position);
                if (distance <= triggerDistance)
                {
                    isSpawnPointActive[j][i] = false;
                    for (int k = 0; k < spawnPositions.Count; k++)
                    {
                        TriggerRandomSpawnManager(spawnPositions[k], spawnRotation, k);
                        Debug.Log($"Spawn triggered at {spawnPoint.name}");
                    }
                }
            }
        }
    }

    private List<Vector3> FixSpawnPoint(Vector3 spawnPosition, int stage)
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        int randomChoice = 0;
        int iter = Random.Range(2, 4);
        List<Vector3> fixedSpawnPositions = new List<Vector3>();
        Vector3 fixedSpawnPosition;
        int prev1 = -1;
        int prev2 = -1;
        for (int i = 0; i < iter; i++)
        {
            while (randomChoice == prev1 || randomChoice == prev2)
                randomChoice = Random.Range(0, 3);

            if (stage == 0)
            {
                fixedSpawnPosition = spawnPosition - new Vector3(2 * randomChoice + 1, 0, 0);
            }
            else if (stage == 1)
            {
                fixedSpawnPosition = spawnPosition - new Vector3(0, 0, 2 * randomChoice + 1);
            }
            else
            {
                fixedSpawnPosition = spawnPosition - new Vector3(2 * randomChoice, 0, 0);
            }
            fixedSpawnPositions.Add(fixedSpawnPosition);
            prev2 = prev1;
            prev1 = randomChoice;
        }
        return fixedSpawnPositions;
    }

    private void TriggerRandomSpawnManager(Vector3 spawnPosition, Quaternion spawnRotation, int k)
    {
        Random.InitState(System.DateTime.Now.Millisecond * (k + 1));
        int randomChoice = Random.Range(0, 10);
        if (randomChoice < 3)
        {
            Destroy(Instantiate(itemPrefabs[0 % prefabNum], spawnPosition, spawnRotation), 15f);
        }
        else if (randomChoice < 6)
        {
            Destroy(Instantiate(itemPrefabs[1 % prefabNum], spawnPosition, spawnRotation), 15f);
        }
        else if (randomChoice < 8)
        {
            Destroy(Instantiate(itemPrefabs[2 % prefabNum], spawnPosition, spawnRotation), 15f);
        }
        else if (randomChoice == 8)
        {
            Destroy(Instantiate(itemPrefabs[3 % prefabNum], spawnPosition, spawnRotation), 15f);
        }
        else
        {
            if (isScore)
            {
                spawnRotation *= Quaternion.Euler(0, -90, 0);
                spawnPosition += new Vector3(0.4f, -0.5f, 0);
            }
            Destroy(Instantiate(itemPrefabs[4 % prefabNum], spawnPosition, spawnRotation), 15f);
        }
    }
}
