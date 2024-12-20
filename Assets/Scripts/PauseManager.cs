using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Image Img;
    public RawImage board;
    private RectTransform boardRt;

    // Start is called before the first frame update
    void Start()
    {
        boardRt = board.GetComponent<RectTransform>();
        boardRt.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update() { }

    public void InvokePauseBoard()
    {
        GameManager.instance.setState(State.Pause);
        Time.timeScale = 0f;
        StartCoroutine(ShowBoard());
    }

    IEnumerator ShowBoard()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.unscaledDeltaTime;
            boardRt.localScale = Vector3.Lerp(
                new Vector3(0, 0, 0),
                new Vector3(1, 1, 0),
                elapsedTime / 0.5f
            );
            yield return null;
        }
    }

    public void ClickResume()
    {
        StartCoroutine(HideBoard());
    }

    IEnumerator HideBoard()
    {
        Debug.Log("HIde");
        float elapsedTime = 0.0f;

        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.unscaledDeltaTime;
            boardRt.localScale = Vector3.Lerp(
                new Vector3(1, 1, 0),
                new Vector3(0, 0, 0),
                elapsedTime / 0.5f
            );
            yield return null;
        }
        Time.timeScale = 1f;
        GameManager.instance.setState(State.Play);
    }

    public void ClickRestart()
    {
        StartCoroutine(FadeOut());
        StartCoroutine(LoadThis());
    }

    public void ClickQuit()
    {
        StartCoroutine(FadeOut());
        StartCoroutine(LoadMain());
    }

    IEnumerator LoadMain()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        Time.timeScale = 1f;
        GameManager.instance.setState(State.Ready);
        Debug.Log("Returning to Main Menu...");
        SceneManager.LoadScene("Main");
    }

    IEnumerator LoadThis()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        Time.timeScale = 1f;
        GameManager.instance.setState(State.Play);
        Debug.Log("Restarting the Game...");
        SceneManager.LoadScene("MainScene");
    }

    public void Fadein()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        float fade = 0.0f;

        Img.gameObject.SetActive(true);
        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.unscaledDeltaTime;
            fade = Mathf.Lerp(1, 0, elapsedTime);
            Img.color = new Color(0, 0, 0, fade);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;
        float fade = 0.0f;

        Img.gameObject.SetActive(true);
        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.unscaledDeltaTime;
            fade = Mathf.Lerp(0, 1, elapsedTime);
            Img.color = new Color(0, 0, 0, fade);
            yield return null;
        }
    }
}
