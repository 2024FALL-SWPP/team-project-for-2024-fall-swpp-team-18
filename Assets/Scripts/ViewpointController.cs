using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewpointController : MonoBehaviour
{
    private float rotateSpeed = 400f; 
    private float mouseSpeed = 8f; // 회전속도(mouse 이동시)
    private float mouseX = 0f; // 수평 회전값
    private Quaternion tmp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tmp = Quaternion.Euler(new Vector3(20, 0, 0)) * transform.parent.rotation;

        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            mouseX = 0f;
        }
        if (Input.GetKey(KeyCode.LeftAlt)) {
            mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
            mouseX = Mathf.Clamp(mouseX, -150, 150);

            transform.localEulerAngles = new Vector3(20, mouseX, 0);
        } else {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, tmp, rotateSpeed * Time.deltaTime);
        }

    }
}
