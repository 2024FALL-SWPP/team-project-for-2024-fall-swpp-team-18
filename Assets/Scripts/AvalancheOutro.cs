using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvalancheOutro : MonoBehaviour
{
    public Image Img;
    public RawImage scoreBoard;
    public Vector2 boardSize = new Vector2(1024, 1024);
    private RectTransform boardRt;

    // Start is called before the first frame update

    void Start()
    {
        Img.CrossFadeAlpha(0.0f, 1.0f, false);
        boardRt = scoreBoard.GetComponent<RectTransform>();
        boardRt.sizeDelta = new Vector2(0, 0);
        Invoke("InvokeShowBoard", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InvokeShowBoard() 
    {
        StartCoroutine(ShowBoard(boardSize));
    }

    IEnumerator ShowBoard(Vector2 boardSize)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < 1.0f) {
            elapsedTime += Time.deltaTime;
            boardRt.sizeDelta = Vector2.Lerp(new Vector2(0, 0), boardSize, elapsedTime);
            yield return null;
        }
    }
}
