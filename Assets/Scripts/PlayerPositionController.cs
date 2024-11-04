using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionController : MonoBehaviour
{
    private float Speed = 20.0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * Speed * Time.deltaTime);
        //transform.Rotate(new Vector3(0, 1f, 0) * 10 * Time.deltaTime);
        if (Input.GetKey(KeyCode.Q)) {
            transform.Translate (-transform.right * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E)) {
            transform.Translate (transform.right * Speed * Time.deltaTime);
        }
    }
}
