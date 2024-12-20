using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleController : MonoBehaviour
{
    private float rotateSpeed = 100f; 
    private float speed = 4f; // 회전속도
    private float mouseX = 0f; // 수평 회전값
    private Quaternion tmp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        tmp = Quaternion.Euler(new Vector3(-90, 0, 0));
        mouseX = Mathf.Clamp(mouseX, -30, 30);
        if (GameManager.instance.getState() != State.Pause) {
            if (Input.GetKey(KeyCode.Q)) {
                mouseX -= speed;
                transform.localEulerAngles = new Vector3(0, mouseX, 0) + new Vector3(-90, 0, 0);
            } else if (Input.GetKey(KeyCode.E)) {
                mouseX += speed;
                transform.localEulerAngles = new Vector3(0, mouseX, 0) + new Vector3(-90, 0, 0);
            } else {
                mouseX = 0;
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, tmp, rotateSpeed * Time.deltaTime);
            }
        }
    }
}
