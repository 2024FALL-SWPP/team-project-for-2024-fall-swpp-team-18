using UnityEngine;
using UnityEngine.UI;

public class GaugeAvalancheController : MonoBehaviour
{
    public Transform playerTransform1; // 플레이어 Transform
    public Transform playerTransform3; // 플레이어 Transform
    public Transform startPoint; // 출발지 Transform
    public Transform destination; // 목적지 Transform
    public GameObject gaugePlayer; // UI 이미지 오브젝트

    private RectTransform gaugeRect; // UI RectTransform
    public GameObject player;
    private float totalDistance; // 출발지와 목적지 사이 거리

    void Start()
    {
        // RectTransform 가져오기
        gaugeRect = gaugePlayer.GetComponent<RectTransform>();
        if (gaugeRect == null)
        {
            Debug.LogError("GaugePlayer is missing RectTransform!");
            return;
        }

        // Transform 체크
        if (
            playerTransform1 == null
            || playerTransform3 == null
            || startPoint == null
            || destination == null
        )
        {
            Debug.LogError("Transform references are missing in the Inspector!");
            return;
        }

        // 총 거리 계산
        totalDistance = Vector3.Distance(startPoint.position, destination.position);
    }

    void Update()
    {
        PlayerPositionController playerController = player.GetComponent<PlayerPositionController>();

        if (playerController.getStage() == 1)
        {
            // 현재 거리 계산
            float currentDistance = Vector3.Distance(
                playerTransform1.position,
                destination.position
            );
            float gaugeRatio = 1 - Mathf.Clamp01(currentDistance / totalDistance);

            if (totalDistance > 0) // totalDistance가 0 이상일 때만 계산
            {
                UpdateGauge(gaugeRatio);
            }
            else
            {
                Debug.LogWarning("Total distance is 0 or less. Cannot calculate gauge ratio.");
            }
        }

        if (playerController.getStage() == 3)
        {
            // 현재 거리 계산
            float currentDistance = Vector3.Distance(
                playerTransform3.position,
                destination.position
            );
            float gaugeRatio = 1 - Mathf.Clamp01(currentDistance / totalDistance);

            if (totalDistance > 0) // totalDistance가 0 이상일 때만 계산
            {
                UpdateGauge(gaugeRatio);
            }
            else
            {
                Debug.LogWarning("Total distance is 0 or less. Cannot calculate gauge ratio.");
            }
        }
    }

    private void UpdateGauge(float gaugeRatio)
    {
        // 게이지 Width 업데이트
        float newWidth = Mathf.Lerp(0, 2000, gaugeRatio);

        gaugeRect.sizeDelta = new Vector2(newWidth, gaugeRect.sizeDelta.y);
    }
}
