using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform spawnPoint;
    public KeyCode spawnKey = KeyCode.Space;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnFireball();
        }
    }

    void SpawnFireball()
    {
        if (fireballPrefab != null && spawnPoint != null)
        {
            Instantiate(fireballPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Fireball Prefab or Spawn Point is not assigned!");
        }
    }
}
