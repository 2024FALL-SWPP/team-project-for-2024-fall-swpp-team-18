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
    public bool shake = false;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // 초기 위치
        init = Quaternion.Euler(transform.parent.right * 15) * transform.parent.rotation;

        // 진동 기능 구현
        transform.localPosition = origin;
        if (shake)
        {
            origin = transform.localPosition;
            transform.localPosition = origin + (Vector3)Random.insideUnitCircle;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            mouseX = 0f;
        }
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
            mouseX = Mathf.Clamp(mouseX, -150, 150);

            transform.localEulerAngles = new Vector3(20, mouseX, 0);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                init,
                rotateSpeed * Time.deltaTime
            );
        }
    }

    IEnumerator Shake(float t)
    {
        shake = true;
        yield return new WaitForSeconds(t);
        shake = false;
    }

    public void Shake_t(float t)
    {
        StartCoroutine(Shake(t));
    }
}
