using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Palette : StudentItem
{
    public CanvasController canvasController; // CanvasController 연결

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
