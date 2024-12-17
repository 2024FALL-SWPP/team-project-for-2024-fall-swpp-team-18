using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Outro1 : MonoBehaviour
{
    private Animator animator;
    public GameObject Cam;
    public RawImage scoreBoard;
    public Text gameOver, menuButton;
    public Image Img;
    private RectTransform boardRt;
    private float T = 0.0f;
    private bool fadein = false, jump = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        boardRt = scoreBoard.GetComponent<RectTransform>();
        boardRt.localScale = new Vector3(0, 0, 0);
        gameOver.color = new Color(1, 1, 1, 0);
        menuButton.color = new Color(1, 1, 1, 0);
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

    public void OnClickStartButton()
    {
        Img.gameObject.SetActive(true);
        Img.CrossFadeAlpha(1.0f, 1.0f, false);
        Invoke("LoadMain", 1.0f);
    }

    private void LoadMain() {
        SceneManager.LoadScene("Main");
    }

    public void OnHoverEnter()
    {
        menuButton.color = new Color(0.7f, 0.7f, 0.7f, 1);
    }

    public void OnHoverExit()
    {
        menuButton.color = new Color(1, 1, 1, 1);
    }

    private void InvokeShowBoard() 
    {
        StartCoroutine(ShowBoard());
    }

    IEnumerator ShowBoard()
    {
        float elapsedTime = 0.0f;
        float fade = 0.0f;

        while (elapsedTime < 0.5f) {
            elapsedTime += Time.deltaTime;
            boardRt.localScale = Vector3.Lerp(new Vector3(0, 0), new Vector3(1, 1, 0), elapsedTime/0.5f);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(0.5f);
        elapsedTime = 0.0f;
        while (elapsedTime < 0.5f) {
            elapsedTime += Time.deltaTime;
            fade = Mathf.Lerp(0, 1, elapsedTime/0.5f);
            gameOver.color = new Color(1, 1, 1, fade);
            yield return null;
        }
        GameObject.Find("ScoreBoard").GetComponent<ScoreUIController>().InvokeShowTextsSequentially();
        elapsedTime = 0.0f;
        while (elapsedTime < 0.5f) {
            elapsedTime += Time.deltaTime;
            fade = Mathf.Lerp(0, 1, elapsedTime/0.5f);
            menuButton.color = new Color(1, 1, 1, fade);
            yield return null;
        }
        Img.gameObject.SetActive(false);
    }
}
