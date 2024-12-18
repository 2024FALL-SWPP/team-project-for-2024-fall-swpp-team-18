using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPositionController : MonoBehaviour
{
    [SerializeField]
    public float Speed = 5.0f;

    [SerializeField]
    public GameObject Avalanche2;
    public Image Img;
    private float ItemDuration = 5f;
    private bool BumpWallLeft = false;
    private bool BumpWallRight = false;
    private bool BumpSnowflake = false;
    private bool BumpHurricane = false;
    private bool Stop = false;
    private int stage = 1;
    private Vector3 CurForward;
    private Coroutine activeRecoveryCoroutine = null; // 현재 활성화된 Snowflake 코루틴
    private Coroutine activeHurricaneCoroutine = null; // 현재 활성화된 Hurricane 코루틴
    private GameObject scoreManager;
    private ScoreManager scoreManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerRb = GetComponent<Rigidbody>();
        scoreManager = GameObject.Find("ScoreManager");
        scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
        GameObject.Find("PauseManager").GetComponent<PauseManager>().Fadein();
        Invoke("RemoveBlackScreen", 1.0f);
    }

    public int getStage()
    {
        return stage;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GameObject.Find("PauseManager").GetComponent<PauseManager>().InvokePauseBoard();
        }
        // 눈송이 또는 허리케인 효과가 없을 때만 이동
        if (!BumpSnowflake && !BumpHurricane && !Stop)
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

        GameObject Snowball = GameObject.FindWithTag("Snowball");
        if (
            Snowball != null
            && Vector3.Distance(transform.position, Snowball.transform.position) <= 10.0f
        )
        {
            //GameObject.Find("Main Camera").GetComponent<ViewpointController>().Shake_t(10.0f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Stop = true;
            GameObject.Find("Main Camera").GetComponent<ViewpointController>().Shake_t(1f);
            SFXController.PlayExplosion();
            scoreManagerScript.DecreaseHeart();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WallLeft"))
        {
            BumpWallLeft = true;
        }
        if (other.gameObject.CompareTag("WallRight"))
        {
            BumpWallRight = true;
        }
        if (other.gameObject.CompareTag("Avalanche"))
        {
            scoreManagerScript.collideAvalanche();
        }
        if (other.gameObject.CompareTag("MainGate"))
        {
            scoreManagerScript.arriveMainGate();
        }
        if (other.gameObject.CompareTag("Snowball"))
        {
            Debug.Log("Collision with snowball");
            SFXController.PlayExplosion();
            GameObject.Find("Main Camera").GetComponent<ViewpointController>().Shake_t(1f);
            scoreManagerScript.collideSnowball();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("JumpBoard"))
        {
            StartCoroutine(Jump());
            GameObject.Find("Player").GetComponent<PlayerController>().JumpControl();
        }
        if (other.gameObject.CompareTag("Corner1"))
        {
            StartCoroutine(TurnCorner1());
            stage = 2;
        }
        if (other.gameObject.CompareTag("Corner2"))
        {
            Avalanche2.SetActive(true);
            StartCoroutine(TurnCorner2());
            stage = 3;
        }
        if (other.gameObject.CompareTag("Professor"))
        {
            SFXController.PlayBlip();
            scoreManagerScript.IncreaseProfessor();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Snowflake") && !BumpHurricane)
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

        if (other.gameObject.CompareTag("Hurricane") && !BumpSnowflake)
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
            activeHurricaneCoroutine = StartCoroutine(
                HandleHurricaneEffect(other.transform.position, other.gameObject)
            );
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WallLeft"))
        {
            BumpWallLeft = false;
        }
        if (other.gameObject.CompareTag("WallRight"))
        {
            BumpWallRight = false;
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Stop = false;
        }
    }

    IEnumerator Jump()
    {
        float t = 0.0f;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            transform.Translate(Vector3.up * Speed * 5 * t * Time.deltaTime);
            yield return null;
        }
        while (t < 1.0f)
        {
            t += Time.deltaTime;
            transform.Translate(Vector3.up * Speed * 5 * (1 - t) * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator TurnCorner1()
    {
        float t = 0.0f;
        while (t < 2.0f)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.Euler(0, 90, 0),
                250 * Time.deltaTime
            );
            yield return null;
        }
    }

    IEnumerator TurnCorner2()
    {
        float t = 0.0f;
        while (t < 10.0f)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.Euler(3.274f, 180, 0),
                250 * Time.deltaTime
            );
            yield return null;
        }
    }

    IEnumerator HandleSnowflakeEffect()
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

    IEnumerator SpinPlayer(float spinSpeed, float spinDuration)
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

    IEnumerator HandleHurricaneEffect(Vector3 hurricaneCenter, GameObject hurricaneObject)
    {
        Debug.Log("Hurricane effect started.");

        // 플레이어를 허리케인 중심으로 이동
        transform.position = hurricaneCenter;

        // 제자리 회전 (Q, E 입력으로 지속 시간 감소)
        float spinSpeed = 540f; // 빠른 회전 속도
        float spinDuration = 3f; // 허리케인 지속 시간
        float escapeReduction = 0.4f; // Q, E 입력 시 지속 시간 감소량
        yield return StartCoroutine(SpinPlayerByKeyboard(spinSpeed, spinDuration, escapeReduction));

        Speed = 10f;
        Debug.Log("Player escaped the hurricane.");

        BumpHurricane = false; // 상태 복구
        activeHurricaneCoroutine = null;

        // 허리케인 오브젝트 삭제
        Destroy(hurricaneObject);
        Debug.Log("Hurricane destroyed.");
    }

    IEnumerator SpinPlayerByKeyboard(float spinSpeed, float spinDuration, float escapeReduction)
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

    public bool getBumpHurricane()
    {
        return BumpHurricane;
    }

    public void CallCoroutine(float delta, GameObject item)
    {
        StartCoroutine(ChangeSpeed(delta, item));
    }

    public IEnumerator ChangeSpeed(float delta, GameObject item)
    {
        Speed += delta;

        BackgroundMusicController.Instance.SetPlaySpeed(1 + (delta / 10));

        yield return new WaitForSeconds(ItemDuration);
        Speed -= delta;
        BackgroundMusicController.Instance.PlayNormal();
        Destroy(item);
    }

    private void RemoveBlackScreen()
    {
        Img.gameObject.SetActive(false);
    }
}
