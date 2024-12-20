using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform spawnPoint;
    public UnityEngine.Vector3 offset = new UnityEngine.Vector3(0, 5, -5);
    private KeyCode spawnKey = KeyCode.Space;
    public GameObject scoreManager;
    public ScoreManager scoreManagerScript;

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
        if (scoreManagerScript.fireball > 0 && fireballPrefab != null && spawnPoint != null)
        {
            UnityEngine.Quaternion newRotation =
                spawnPoint.rotation * UnityEngine.Quaternion.Euler(1.5f, 180f, 0);
            GameObject fireball = Instantiate(
                fireballPrefab,
                spawnPoint.position + offset,
                newRotation
            );
            scoreManagerScript.fireball--;
            Destroy(fireball, 5f);
        }
        else
        {
            Debug.LogWarning("Fireball Prefab or Spawn Point is not assigned!");
        }
    }
}
