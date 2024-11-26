using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 300.0f; 
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
        Quaternion tmp = transform.parent.rotation;
        if (Input.GetKey(KeyCode.Q)) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.parent.forward * 15)*tmp, speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.E)) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.parent.forward * -15)*tmp, speed * Time.deltaTime);
        } else {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, tmp, 100 * Time.deltaTime);
        }
    }
}
