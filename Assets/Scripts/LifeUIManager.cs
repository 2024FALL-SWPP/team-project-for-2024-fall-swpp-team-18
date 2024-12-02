using System.Collections.Generic;
using UnityEngine;

public class LifeUIManager : MonoBehaviour
{
    public GameObject heartPrefab; // 하트 Prefab (Transform만 사용하는 Prefab)
    public Transform heartsContainer; // 하트를 배치할 Panel (UI Container)
    public int maxLives = 3; // 최대 생명 수
    private List<GameObject> hearts = new List<GameObject>();

    private int currentLives;

    public Vector3[] heartPositions; // 하트의 UI 좌표를 지정할 배열

    void Start()
    {
        // 초기 생명 설정
        currentLives = maxLives;

        // 하트 위치 배열이 maxLives와 맞지 않으면 오류 출력
        if (heartPositions.Length != maxLives)
        {
            Debug.LogError("Heart positions array size must match maxLives.");
            return;
        }

        UpdateLivesUI();
    }

    // 생명 감소
    public void DecreaseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            UpdateLivesUI();
        }
    }

    // 생명 증가
    public void IncreaseLife()
    {
        if (currentLives < maxLives)
        {
            currentLives++;
            UpdateLivesUI();
        }
    }

    // UI 업데이트
    private void UpdateLivesUI()
    {
        // 기존 하트 제거
        foreach (GameObject heart in hearts)
        {
            Destroy(heart);
        }
        hearts.Clear();

        // 현재 생명 수만큼 하트 생성
        for (int i = 0; i < currentLives; i++)
        {
            // 하트를 heartsContainer의 자식으로 생성
            GameObject newHeart = Instantiate(heartPrefab, heartsContainer);

            // Transform을 사용하여 위치 설정
            Transform heartTransform = newHeart.transform;
            heartTransform.localPosition = heartPositions[i];

            hearts.Add(newHeart);
        }
    }
}
