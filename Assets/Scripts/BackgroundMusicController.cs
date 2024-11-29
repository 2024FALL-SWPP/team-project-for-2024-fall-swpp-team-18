using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public static BackgroundMusicController Instance;

    // Inspector에서 설정 가능한 AudioClip
    [Header("Audio Clip")]
    public AudioClip audioClip;

    // AudioSource는 내부에서만 접근
    private AudioSource audioSource;

    [Range(0.1f, 3.0f)]
    public float playbackSpeed = 1.0f;

    private bool isPaused = false;

    private void Awake()
    {
        // Singleton 패턴 초기화
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }

        // AudioSource 초기화
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // AudioClip 설정
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        }
        else
        {
            Debug.LogWarning("AudioClip이 설정되지 않았습니다.");
        }

        // 반복 재생 활성화
        audioSource.loop = true;
        // 재생 시작
        Play();
    }

    private void Update()
    {
        // AudioSource 재생 속도 동기화
        if (audioSource != null)
        {
            audioSource.pitch = playbackSpeed;
        }
    }

    /// <summary>
    /// 음악 재생 시작
    /// </summary>
    public void Play()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            isPaused = false;
            audioSource.Play();
        }
    }

    /// <summary>
    /// 음악 일시정지
    /// </summary>
    public void Pause()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            isPaused = true;
            audioSource.Pause();
        }
    }

    /// <summary>
    /// 음악 완전히 정지
    /// </summary>
    public void Stop()
    {
        if (audioSource != null)
        {
            isPaused = false;
            audioSource.Stop();
        }
    }

    /// <summary>
    /// 현재 음악이 재생 중인지 확인
    /// </summary>
    public bool IsPlaying()
    {
        return audioSource != null && audioSource.isPlaying;
    }
}