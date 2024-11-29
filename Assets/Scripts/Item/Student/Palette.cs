using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Palette : StudentItem
{
    private GameObject canvas;
    private CanvasController canvasController; // CanvasController 연결

    void Start()
    {
        base.Start();
        canvas = GameObject.Find("Canvas");
        canvasController = canvas.GetComponent<CanvasController>();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
        {
            canvasController.ShowRandomImages(3); // 랜덤한 위치에 3개의 이미지를 생성

            Destroy(gameObject);
        }
    }
}
