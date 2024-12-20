using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputTestHelper
{
    private static readonly Dictionary<KeyCode, bool> keyStates = new Dictionary<KeyCode, bool>();
    private static readonly Dictionary<KeyCode, bool> keyDownStates =
        new Dictionary<KeyCode, bool>();
    private static readonly Dictionary<string, float> axisValues = new Dictionary<string, float>();

    public static void SetKey(KeyCode key, bool isPressed)
    {
        if (keyStates.ContainsKey(key))
            keyStates[key] = isPressed;
        else
            keyStates.Add(key, isPressed);
    }

    public static void SetAxis(string axisName, float value)
    {
        if (axisValues.ContainsKey(axisName))
            axisValues[axisName] = value;
        else
            axisValues.Add(axisName, value);
    }

    public static bool GetKey(KeyCode key)
    {
        return keyStates.ContainsKey(key) && keyStates[key];
    }

    public static bool GetKeyDown(KeyCode key)
    {
        if (keyDownStates.ContainsKey(key) && keyDownStates[key])
        {
            keyDownStates[key] = false; // 한 번 호출 후 false로 초기화
            return true;
        }
        return false;
    }

    public static float GetAxis(string axisName)
    {
        return axisValues.ContainsKey(axisName) ? axisValues[axisName] : 0f;
    }

    public static void Reset()
    {
        keyStates.Clear();
        axisValues.Clear();
    }
}

public class ViewpointController : MonoBehaviour
{
    [SerializeField]
    public float rotateSpeed = 400f;

    [SerializeField]
    public float mouseSpeed = 8f;

    [SerializeField]
    public float maxMouseX = 150f;

    [SerializeField]
    public float minMouseX = -150f;

    private float mouseX = 0f;
    private Quaternion initialRotation;
    private Vector3 originPosition;

    void Start()
    {
        originPosition = transform.localPosition;
    }

    public void Update()
    {
        //////////////////////// 테스트 시 주석 처리
        InputTestHelper.SetKey(KeyCode.LeftShift, Input.GetKey(KeyCode.LeftShift)); // Key 상태를 업데이트
        InputTestHelper.SetAxis("Mouse X", Input.GetAxis("Mouse X")); // "Mouse X" 축 값 업데이트
        if (GameManager.instance.getState() != State.Pause) {
            init = Quaternion.Euler(transform.parent.right * 15) * transform.parent.rotation;
        //////////////////////// 테스트 시 주석 처리
        HandleRotation();
    }

    private void HandleRotation()
    {
        // 초기 회전값 설정
        initialRotation = Quaternion.Euler(transform.parent.right * 15) * transform.parent.rotation;

        if (InputTestHelper.GetKeyDown(KeyCode.LeftShift))
        {
            mouseX = 0f;
        }
        if (InputTestHelper.GetKey(KeyCode.LeftShift))
        {
            HandleMouseRotation();
        }
        else
        {
            ResetToInitialRotation();
        }
    }

    private void HandleMouseRotation()
    {
        mouseX += InputTestHelper.GetAxis("Mouse X") * mouseSpeed;
        mouseX = Mathf.Clamp(mouseX, minMouseX, maxMouseX);

        transform.localEulerAngles = new Vector3(15, mouseX, 0);
    }

    private void ResetToInitialRotation()
    {
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            initialRotation,
            rotateSpeed * Time.deltaTime
        );
    }

    IEnumerator Shake(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            if (GameManager.instance.getState() != State.Pause) 
            {
                transform.localPosition = originPosition + (Vector3)Random.insideUnitCircle;
            }

            yield return null;
        }

        transform.localPosition = originPosition;
    }

    public void Shake_t(float t)
    {
        StartCoroutine(Shake(t));
    }
}
