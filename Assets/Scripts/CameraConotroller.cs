using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConotroller : MonoBehaviour
{
    [SerializeField]
    public Transform avalanche;

    [SerializeField]
    public Vector3 offset = new Vector3(0, 5, -30);

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void LateUpdate()
    {
        transform.position = avalanche.position + offset;
    }
}
