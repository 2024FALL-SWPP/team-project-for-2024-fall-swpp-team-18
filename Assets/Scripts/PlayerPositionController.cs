using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionController : MonoBehaviour
{
    //private Rigidbody PlayerRb;
    private float Speed = 10.0f; 
    private bool BumpWallLeft = false;
    private bool BumpWallRight = false;
    private bool Stop = false;
    private Vector3 CurForward;
    public Quaternion BeforeJump;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerRb = gameObject.GetComponent<Rigidbody>();
        BeforeJump = transform.rotation;
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
            BeforeJump = transform.rotation;
        }
        if (other.gameObject.CompareTag("Corner2")) {
            transform.forward = Vector3.back;
            BeforeJump = transform.rotation;
        }
        if (other.gameObject.CompareTag("Avalanche")) {
            GameManager.instance.GameOver = true;
        }
        if (other.gameObject.CompareTag("Obstacle")) {
            Stop = true;
            GameObject.Find("Main Camera").GetComponent<ViewpointController>().Shake_t(1f);
        }
        if (other.gameObject.CompareTag("Snowball")) {
            GameManager.instance.GameOver =true;
        }
        if (other.gameObject.CompareTag("JumpBoard")) { 
            StartCoroutine(Jump());
            GameObject.Find("Player").GetComponent<PlayerController>().JumpControl();
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

    IEnumerator Jump() {
        float t = 0.0f;
        while (t < 0.5f) {
            t += Time.deltaTime;
            transform.Translate(Vector3.up * Speed * 2 * t * Time.deltaTime);
            yield return null;
        }
        while (t < 1.0f) {
            t += Time.deltaTime;
            transform.Translate(Vector3.up * Speed * 2 * (1-t) * Time.deltaTime);
            yield return null;
        }
    
    }
}
