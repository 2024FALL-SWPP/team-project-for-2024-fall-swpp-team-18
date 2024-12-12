using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewpointController : MonoBehaviour
{
    private float rotateSpeed = 400f; 
    private float mouseSpeed = 8f; // 회전속도(mouse 이동시)
    private float mouseX = 0f; // 수평 회전값
    private Quaternion init;
    private Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // 초기 위치
        init = Quaternion.Euler(transform.parent.right*20) * transform.parent.rotation;

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            mouseX = 0f;
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
            mouseX = Mathf.Clamp(mouseX, -150, 150);

            transform.localEulerAngles = new Vector3(20, mouseX, 0);
        } else {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, init, rotateSpeed * Time.deltaTime);
        }

    }

    IEnumerator Shake(float t) {
        float T = 0.0f;
        while(T < t) {
            T += Time.deltaTime;
            transform.localPosition = origin + (Vector3)Random.insideUnitCircle;
            yield return null;
        }
        transform.localPosition = origin;
    }

    public void Shake_t(float t) {
        StartCoroutine(Shake(t));
    }
}
