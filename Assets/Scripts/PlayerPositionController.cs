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
        
        Speed = 10f;

        BumpSnowflake = false; // 상태 복구
        activeRecoveryCoroutine = null; // 활성화된 Snowflake 코루틴 해제
        Debug.Log("Player fully recovered from snowflake.");
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

    private IEnumerator HandleHurricaneEffect(Vector3 hurricaneCenter, GameObject hurricaneObject)
    {
    Debug.Log("Hurricane effect started.");


    // 플레이어를 허리케인 중심으로 이동
    transform.position = hurricaneCenter;
    

    // 제자리 회전 (Q, E 입력으로 지속 시간 감소)
    float spinSpeed = 540f; // 빠른 회전 속도
    float spinDuration = 3f; // 허리케인 지속 시간
    float escapeReduction = 0.2f; // Q, E 입력 시 지속 시간 감소량
    yield return StartCoroutine(SpinPlayerByKeyboard(spinSpeed, spinDuration, escapeReduction));

    Speed = 10f;
    Debug.Log("Player escaped the hurricane.");

    BumpHurricane = false; // 상태 복구
    activeHurricaneCoroutine = null;
    // 허리케인 오브젝트 삭제
    Destroy(hurricaneObject);
    Debug.Log("Hurricane destroyed.");
    }



    

    private IEnumerator SpinPlayerByKeyboard(float spinSpeed, float spinDuration, float escapeReduction)
    {
    Debug.Log("SpinPlayerByKeyboard started.");

    float elapsedTime = 0f;
    Quaternion initialRotation = transform.rotation;

    while (elapsedTime < spinDuration)
    {
        // 플레이어는 항상 제자리 회전
        float angle = spinSpeed * Time.deltaTime;
        transform.Rotate(0f, angle, 0f);

        // Q, E 입력 처리
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            spinDuration -= escapeReduction;
            Debug.Log($"Escape time reduced! Remaining spin duration: {spinDuration}");
        }

        elapsedTime += Time.deltaTime;
        
        yield return null; // 다음 프레임까지 대기
    }
    transform.rotation = initialRotation;
    Debug.Log("SpinPlayerByKeyboard finished.");
    }

}