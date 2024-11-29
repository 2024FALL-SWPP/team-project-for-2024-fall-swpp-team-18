using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform spawnPoint;
    public Vector3 offset = new Vector3(0, 5, -5);
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
            Quaternion newRotation = spawnPoint.rotation * Quaternion.Euler(0, 180f, 0);
            Instantiate(fireballPrefab, spawnPoint.position + offset, newRotation);
        }
        else
        {
            Debug.LogWarning("Fireball Prefab or Spawn Point is not assigned!");
        }
    }
}
