using System.Collections;
using UnityEngine;

public class PlayerPositionController : MonoBehaviour
{
    public float Speed = 10.0f;

    private bool BumpWallLeft = false;
    private bool BumpWallRight = false;
    private bool BumpSnowflake = false; 
    private bool BumpHurricane = false; 
    private Coroutine activeRecoveryCoroutine = null; // 현재 활성화된 Snowflake 코루틴
    private Coroutine activeHurricaneCoroutine = null; // 현재 활성화된 Hurricane 코루틴

    void Update()
    {
        // 눈송이 또는 허리케인 효과가 없을 때만 이동
        if (!BumpSnowflake && !BumpHurricane)
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

            // Snowflake 효과 중복 방지
            if (activeRecoveryCoroutine != null)
            {
                StopCoroutine(activeRecoveryCoroutine); // 기존 Snowflake 효과 중단
                Debug.Log("Existing snowflake effect interrupted.");
            }

            // 새로운 Snowflake 효과 시작
            BumpSnowflake = true;
            Speed = 0.0f; // 속도 0으로 설정
            activeRecoveryCoroutine = StartCoroutine(HandleSnowflakeEffect());
        }

        if (other.gameObject.CompareTag("Hurricane"))
        {
            Debug.Log("Collision with hurricane detected.");

            // Hurricane 효과 중복 방지
            if (activeHurricaneCoroutine != null)
            {
                StopCoroutine(activeHurricaneCoroutine); // 기존 Hurricane 효과 중단
                Debug.Log("Existing hurricane effect interrupted.");
            }

            // 새로운 Hurricane 효과 시작
            BumpHurricane = true;
            Speed = 0.0f; // 속도 0으로 설정
            activeHurricaneCoroutine = StartCoroutine(HandleHurricaneEffect(other.transform.position, other.gameObject));
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

        // 제자리 회전
        float spinSpeed = 360f; // 회전 속도
        float spinDuration = 1f; // 회전 지속 시간
        yield return StartCoroutine(SpinPlayer(spinSpeed, spinDuration));
        
        // 속도 복구
        float targetSpeed = 10f;
        Speed = targetSpeed;

        BumpSnowflake = false; // 상태 복구
        activeRecoveryCoroutine = null; // 활성화된 Snowflake 코루틴 해제
        Debug.Log("Player fully recovered from snowflake.");
    }

    private IEnumerator HandleHurricaneEffect(Vector3 hurricaneCenter, GameObject hurricaneObject)
    {
    Debug.Log("Hurricane effect started.");

    // 현재 회전 상태 저장
    

    // 허리케인 중심으로 이동
    Vector3 initialPosition = transform.position;
    transform.position = hurricaneCenter;
    Speed = 0f; // 이동 중지

    // 제자리 회전
    float spinSpeed = 540f; // 빠른 회전 속도
    float spinDuration = 3f; // 허리케인 지속 시간
    float escapeReduction = 0.1f; // Q, E 입력 시 지속 시간 감소량
    Quaternion initialRotation = transform.rotation;
    while (spinDuration > 0)
    {
        // 매 프레임 회전
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);

        // Q, E 입력 처리
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            spinDuration -= escapeReduction;
            Debug.Log($"Escape time reduced! Remaining time: {spinDuration}");
        }

        spinDuration -= Time.deltaTime; // 남은 시간 감소
        yield return null;
    }
    transform.position = initialPosition;
    // 허리케인 탈출 후 초기 회전 상태 복원
    transform.rotation = initialRotation;
    Debug.Log("Player escaped the hurricane.");
    Speed = 10f; // 속도 복구
    BumpHurricane = false; // 상태 복구

    // 허리케인 오브젝트 삭제
    Destroy(hurricaneObject);
    Debug.Log("Hurricane destroyed.");
    }




    private IEnumerator SpinPlayer(float spinSpeed, float spinDuration)
    {
        Debug.Log("Player spinning due to effect!");
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