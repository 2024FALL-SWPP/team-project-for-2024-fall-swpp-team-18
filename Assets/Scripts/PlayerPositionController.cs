using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionController : MonoBehaviour
{
    private float Speed = 10.0f; 

    private bool BumpWallLeft = false;
    private bool BumpWallRight = false;
    private Vector3 CurForward; 
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Q) && !BumpWallLeft) {
            transform.Translate (-Vector3.right * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E) && !BumpWallRight) {
            transform.Translate (Vector3.right * Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("WallLeft")) {
            BumpWallLeft = true;
        }
        if (other.gameObject.CompareTag("WallRight")) {
            BumpWallRight = true;
        }
        if (other.gameObject.CompareTag("Corner1")) {
            transform.forward = Vector3.right;
        }
        if (other.gameObject.CompareTag("Corner2")) {
            transform.forward = Vector3.back;
        }
    }    
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("WallLeft")) {
            BumpWallLeft = false;
        }
        if (other.gameObject.CompareTag("WallRight")) {
            BumpWallRight = false;
        }
    }
}
