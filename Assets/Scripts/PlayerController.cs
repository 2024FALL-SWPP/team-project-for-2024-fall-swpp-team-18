using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 300.0f; 
    //public Quaternion tiltAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Quaternion upAngle = Quaternion.Euler(transform.parent.up);
        //Quaternion leftAngle = Quaternion.Euler(transform.parent.right);
        //tiltAngle = Quaternion.Lerp(transform.parent.rotation, Quaternion.Euler(new Vector3(0,0,45)), 0.3f);    
        
        if (Input.GetKey(KeyCode.Q)) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0,0,15)), speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.E)) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0,0,-15)), speed * Time.deltaTime);
        } else {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.parent.rotation, 100 * Time.deltaTime);
        }
    }
}
