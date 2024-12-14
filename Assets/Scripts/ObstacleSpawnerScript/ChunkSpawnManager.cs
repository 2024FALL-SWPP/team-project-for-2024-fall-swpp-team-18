using UnityEngine;
using System.Collections;

public class ObstacleFactory
{
    private GameObject[] chunkPrefabs;

    public ObstacleFactory(GameObject[] chunkPrefabs)
    {
        this.chunkPrefabs = chunkPrefabs;
    }

    public GameObject CreateRandomObstacle()
    {
        // 무작위로 장애물 선택
        int randomIndex = Random.Range(0, chunkPrefabs.Length);
        return chunkPrefabs[randomIndex];
    }
}


public class ChunkSpawnManager : MonoBehaviour
{
    public GameObject[] chunkPrefabs; 
    public Transform[] spawnPoints; 
    public float spawnDistance = 10.0f; 
    public float checkInterval = 0.5f; 

    private GameObject player; 
    private ObstacleFactory obstacleFactory; 

    private void Start()
    {
     
        obstacleFactory = new ObstacleFactory(chunkPrefabs);

        
        player = GameObject.FindGameObjectWithTag("Player");

      
        if (player != null)
        {
            StartCoroutine(SpawnObstacles());
        }
        else
        {
            Debug.LogError("Player 태그를 가진 오브젝트를 찾을 수 없습니다!");
        }
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
               
                float distance = Vector3.Distance(player.transform.position, spawnPoint.position);

             
                if (distance <= spawnDistance)
                {
                   
                    GameObject randomObstacle = obstacleFactory.CreateRandomObstacle();

                 
                    Instantiate(randomObstacle, spawnPoint.position, spawnPoint.rotation);

             
                    yield return new WaitForSeconds(1.0f);
                }
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }
}
