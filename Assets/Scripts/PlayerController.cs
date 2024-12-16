using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 100.0f; 
    private bool onJump = false;
    //public Quaternion tiltAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Quaternion tmp = transform.parent.rotation;
        if (Input.GetKey(KeyCode.Q) && !onJump) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.parent.forward * 15)*tmp, speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.E) && !onJump) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.parent.forward * -15)*tmp, speed * Time.deltaTime);
        } else {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, tmp, 100 * Time.deltaTime);
        }
    }

    IEnumerator Jump() {
        onJump = true;
        float t = 0.0f;
        while (t < 1.0f) {
            t += Time.deltaTime;
            transform.Rotate(Vector3.right * 300 * (t-0.5f) * Time.deltaTime);
            yield return null;
        }
        onJump = false;
    }

    public void JumpControl() {
        StartCoroutine(Jump());
    }
}
