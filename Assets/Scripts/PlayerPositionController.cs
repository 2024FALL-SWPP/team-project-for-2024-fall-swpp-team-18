using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionController : MonoBehaviour
{
    private float Speed = 10.0f; 
    public Vector3 show;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        show = transform.forward;  
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Q)) {
            transform.Translate (-Vector3.right * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E)) {
            transform.Translate (Vector3.right * Speed * Time.deltaTime);
        }
    }
}
