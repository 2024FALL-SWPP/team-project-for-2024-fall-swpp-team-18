using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnerManager : MonoBehaviour
{
    public CarSpawnManager carSpawnManager; // CarSpawnManager 참조
    public PeopleSpawnManager peopleSpawnManager; // PeopleSpawnManager 참조
    public TreeSpawnManager treeSpawnManager; // TreeSpawnManager 참조

    public JumpSpawnManager jumpSpawnManager; // TreeSpawnManager 참조

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TriggerRandomSpawnManager", 0, 3);
    }

    // Update is called once per frame
    void Update() { }

    private void TriggerRandomSpawnManager()
    {
        int randomChoice = Random.Range(0, 4); // 무작위 선택

        if (randomChoice == 0 && carSpawnManager != null)
        {
            // CarSpawnManager 실행
            carSpawnManager.TriggerSpawn(this.transform);
            Debug.Log($"Spawn triggered at {this.transform.name} using {"CarSpawnManager"}");
        }
        else if (randomChoice == 1 && peopleSpawnManager != null)
        {
            // PeopleSpawnManager 실행
            peopleSpawnManager.TriggerSpawn(this.transform);
            Debug.Log($"Spawn triggered at {this.transform.name} using {"PeopleSpawnManager"}");
        }
        else if (randomChoice == 2 && treeSpawnManager != null)
        {
            // PeopleSpawnManager 실행
            treeSpawnManager.TriggerSpawn13(this.transform);
            Debug.Log($"Spawn triggered at {this.transform.name} using {"TreeSpawnManager"}");
        }
        else if (randomChoice == 3 && jumpSpawnManager != null)
        {
            // PeopleSpawnManager 실행
            jumpSpawnManager.TriggerSpawn13(this.transform);
            Debug.Log($"Spawn triggered at {this.transform.name} using {"jumpSpawnManager"}");
        }
    }
}
