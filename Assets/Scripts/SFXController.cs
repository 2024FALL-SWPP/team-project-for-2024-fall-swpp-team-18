using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController Instance;

    // 사전에 고정된 효과음 클립
    public AudioClip blipSound;
    public AudioClip explosionSound;

    private AudioSource audioSource;

    void Awake()
    {
        // 싱글톤 패턴 초기화
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // AudioSource 초기화
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// 특정 효과음을 재생
    /// </summary>
    private void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("재생하려는 AudioClip이 null입니다.");
        }
    }

    /// <summary>
    /// 블립 효과음 재생
    /// </summary>
    public static void PlayBlip()
    {
        if (Instance != null)
        {
            Instance.PlaySFX(Instance.blipSound);
        }
    }

    /// <summary>
    /// 폭발 효과음 재생
    /// </summary>
    public static void PlayExplosion()
    {
        if (Instance != null)
        {
            Instance.PlaySFX(Instance.explosionSound);
        }
    }

}