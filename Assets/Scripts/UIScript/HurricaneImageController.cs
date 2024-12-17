using UnityEngine;
using UnityEngine.UI;

public class HurricaneImageController : MonoBehaviour
{
    public Image Q_Image; // Q 관련 이미지
    public Image E_Image; // E 관련 이미지

    private PlayerPositionController playerController; // PlayerPositionController 참조
    private bool isQOrange = true; // Q 이미지의 현재 색상 상태

    private float blinkInterval = 0.2f; // 깜빡이는 간격 (초)
    private Color orangeColor = new Color(1f, 0.5f, 0f, 1f); // 주황색 (RGBA)
    private Color grayColor = new Color(0.5f, 0.5f, 0.5f, 1f); // 회색 (RGBA)
    private bool isBlinking = false; // 깜빡이는 상태를 추적

    void Start()
    {
        // Player 오브젝트의 최상위 부모를 찾아 PlayerPositionController 가져오기
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
        
            playerController = playerObject.GetComponent<PlayerPositionController>();

            if (playerController == null)
            {
                Debug.LogError("PlayerPositionController script is not attached to the root object of the Player.");
            }
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found in the scene.");
        }

        // 처음에는 이미지를 숨김
        Q_Image.gameObject.SetActive(false);
        E_Image.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerController != null)
        {
            bool bumpHurricane = playerController.getBumpHurricane();
            

            if (bumpHurricane)
            {
                if (!Q_Image.gameObject.activeSelf || !E_Image.gameObject.activeSelf)
                {
                    // BumpHurricane이 true면 이미지를 활성화
                    Q_Image.gameObject.SetActive(true);
                    E_Image.gameObject.SetActive(true);

                    // 이미지를 깜빡이게 만듦
                    if (!isBlinking)
                    {
                        StartCoroutine(BlinkImages());
                    }
                }
            }
            else
            {
                // BumpHurricane이 false면 이미지를 숨김
                if (Q_Image.gameObject.activeSelf || E_Image.gameObject.activeSelf)
                {
                    Q_Image.gameObject.SetActive(false);
                    E_Image.gameObject.SetActive(false);
                    isBlinking = false;
                    StopAllCoroutines(); // 깜빡이기 중단
                }
            }
        }
    }

    private System.Collections.IEnumerator BlinkImages()
    {
        isBlinking = true;

        while (playerController != null && playerController.getBumpHurricane())
        {
            // Q_Image의 색상 설정
            Q_Image.color = isQOrange ? orangeColor : grayColor;

            // E_Image의 색상 설정
            E_Image.color = isQOrange ? grayColor : orangeColor;

            // 색상 토글
            isQOrange = !isQOrange;

            yield return new WaitForSeconds(blinkInterval); // 깜빡이는 간격 대기
        }

        isBlinking = false;
    }
}
