using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionController : MonoBehaviour
{
    private float Speed = 10.0f; 

    private bool BumpWallLeft = false;
    private bool BumpWallRight = false;
    private bool Stop = false;
    private Vector3 CurForward; 
    public bool GameOver = false;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (!Stop) {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q) && !BumpWallLeft) {
            transform.Translate (-Vector3.right * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E) && !BumpWallRight) {
            transform.Translate (Vector3.right * Speed * Time.deltaTime);
        }

        GameObject Snowball = GameObject.FindWithTag("Snowball");
        if (Vector3.Distance(transform.position, Snowball.transform.position) <= 10.0f) {
            GameObject.Find("Main Camera").GetComponent<ViewpointController>().Shake_t(10.0f);
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
        if (other.gameObject.CompareTag("Avalanche")) {
            GameOver = true;
        }
        if (other.gameObject.CompareTag("Obstacle")) {
            Stop = true;
            GameObject.Find("Main Camera").GetComponent<ViewpointController>().Shake_t(1f);
        }
        if (other.gameObject.CompareTag("Snowball")) {
            GameOver =true;
        }
    }    
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("WallLeft")) {
            BumpWallLeft = false;
        }
        if (other.gameObject.CompareTag("WallRight")) {
            BumpWallRight = false;
        }
        if (other.gameObject.CompareTag("Obstacle")) {
            Stop = false;
        }
    }
}
