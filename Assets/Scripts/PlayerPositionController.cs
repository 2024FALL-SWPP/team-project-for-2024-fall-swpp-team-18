using System.Collections;
using UnityEngine;

public class PlayerPositionController : MonoBehaviour
{
    public float Speed = 10.0f;

    private bool BumpWallLeft = false;
    private bool BumpWallRight = false;
    private bool BumpSnowflake = false; 
    private Coroutine activeRecoveryCoroutine = null; // 현재 활성화된 코루틴 추적
    private Vector3 CurForward;

    void Update()
    {
        // 눈송이 효과가 없을 때만 이동
        if (!BumpSnowflake)
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Q) && !BumpWallLeft)
            {
                transform.Translate(-Vector3.right * Speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E) && !BumpWallRight)
            {
                transform.Translate(Vector3.right * Speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WallLeft"))
        {
            BumpWallLeft = true;
        }
        if (other.gameObject.CompareTag("WallRight"))
        {
            BumpWallRight = true;
        }
        if (other.gameObject.CompareTag("Corner1"))
        {
            transform.forward = Vector3.right;
        }
        if (other.gameObject.CompareTag("Corner2"))
        {
            transform.forward = Vector3.back;
        }
        if (other.gameObject.CompareTag("Snowflake"))
        {
            Debug.Log("Collision with snowflake detected.");

            // 눈송이 효과 중복 방지
            if (activeRecoveryCoroutine != null)
            {
                StopCoroutine(activeRecoveryCoroutine); // 기존 효과 중단
                Debug.Log("Existing snowflake effect interrupted.");
            }

            // 새로운 눈송이 효과 시작
            BumpSnowflake = true;
            Speed = 0.0f; // 속도 0으로 설정
            activeRecoveryCoroutine = StartCoroutine(HandleSnowflakeEffect());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WallLeft"))
        {
            BumpWallLeft = false;
        }
        if (other.gameObject.CompareTag("WallRight"))
        {
            BumpWallRight = false;
        }
    }

    private IEnumerator HandleSnowflakeEffect()
    {
        Debug.Log("Snowflake effect started.");

        // 1. 제자리 회전
        float spinSpeed = 360f; // 회전 속도
        float spinDuration = 1f; // 회전 지속 시간
        yield return StartCoroutine(SpinPlayer(spinSpeed, spinDuration));

        // 2. 속도 복구 처리
        float recoveryDuration = 1f; // 속도 복구 지속 시간
        
        float targetSpeed = 10f; // 복구할 최종 속도
        

       
        // 속도 완전히 복구
        Speed = targetSpeed;

        BumpSnowflake = false; // 상태 복구
        activeRecoveryCoroutine = null; // 활성화된 코루틴 해제
        Debug.Log("Player fully recovered from snowflake.");
    }

    private IEnumerator SpinPlayer(float spinSpeed, float spinDuration)
    {
        Debug.Log("Player spinning due to snowflake!");
        float elapsedTime = 0f;
        Quaternion initialRotation = transform.rotation;

        while (elapsedTime < spinDuration)
        {
            // 매 프레임 회전
            float angle = spinSpeed * Time.deltaTime;
            transform.Rotate(0f, angle, 0f);

            elapsedTime += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        // 회전 종료 후 원래 방향 복원
        transform.rotation = initialRotation;
        Debug.Log("Player finished spinning.");
    }
}
