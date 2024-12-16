using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject imagePrefab; // Image 프리팹 연결
    public Canvas canvas; // Canvas 연결
    public Vector2 imageSize = new Vector2(900, 900); // 이미지 크기 설정 (기본값: 100x100)

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void ShowRandomImages(int count)
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 canvasSize = canvasRect.sizeDelta; // Canvas 크기 계산

        for (int i = 0; i < count; i++)
        {
            // 랜덤 위치 생성 (Canvas 기준)
            float randomX = Random.Range(-canvasSize.x / 2, canvasSize.x / 2);
            float randomY = Random.Range(-canvasSize.y / 2, canvasSize.y / 2);
            Vector2 randomPosition = new Vector2(randomX, randomY);

            // 이미지 생성
            GameObject newImage = Instantiate(imagePrefab, canvas.transform);

            // 위치 설정
            RectTransform imageRect = newImage.GetComponent<RectTransform>();
            imageRect.anchoredPosition = randomPosition;

            imageRect.sizeDelta = imageSize;

            Destroy(newImage, 3f);
        }
    }
}
