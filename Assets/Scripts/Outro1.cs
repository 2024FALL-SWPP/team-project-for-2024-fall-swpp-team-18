using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outro1 : MonoBehaviour
{
    private Animator animator;
    public GameObject Cam;
    public RawImage scoreBoard;
    public Vector2 boardSize = new Vector2(1024, 1024);
    private RectTransform boardRt;
    public Image Img;
    private float T = 0.0f;
    private bool fadein = false, jump = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        boardRt = scoreBoard.GetComponent<RectTransform>();
        boardRt.sizeDelta = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        T += Time.deltaTime;
        if (T < 2.0f) {
            if (!fadein) {
                Img.CrossFadeAlpha(0.0f, 2.0f, false);
            }
        } else if (T < 3.8f) {
            Cam.transform.Rotate(Vector3.right * 20.0f * Time.deltaTime);
        } else if (T < 5.0f) {

        } else if (T < 5.6f) {
            Cam.transform.Translate(Vector3.forward * 50.0f * Time.deltaTime);
        } else if (T < 10.0f && !jump) {
            animator.SetBool("Jump", true);
            jump = true;
            Invoke("InvokeShowBoard", 2.0f);
        }
        
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
        GameObject.Find("ScoreBoard").GetComponent<ScoreUIController>().InvokeShowTextsSequentially();
    }
}
