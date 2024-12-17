using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AvalancheOutro : MonoBehaviour
{
    public Image Img;
    public RawImage scoreBoard;
    public Text gameOver, menuButton;
    private RectTransform boardRt;

    // Start is called before the first frame update

    void Start()
    {
        Img.CrossFadeAlpha(0.0f, 1.0f, false);
        boardRt = scoreBoard.GetComponent<RectTransform>();
        boardRt.localScale = new Vector3(0, 0, 0);
        gameOver.color = new Color(1, 1, 1, 0);
        menuButton.color = new Color(1, 1, 1, 0);
        Invoke("InvokeShowBoard", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
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
