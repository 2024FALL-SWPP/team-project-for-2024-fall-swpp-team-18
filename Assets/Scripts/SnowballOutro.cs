using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SnowballOutro : MonoBehaviour
{
    public GameObject Snowball;
    public Image Img;
    public RawImage scoreBoard;
    public Text gameOver, menuButton;
    private RectTransform boardRt;
    private Rigidbody SnowballRb;
    private float T = 0.0f;
    private Vector3 origin;
    private bool fadein = false, board = false;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.localPosition;
        SnowballRb = Snowball.GetComponent<Rigidbody>();
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
            Img.CrossFadeAlpha(0.0f, 1.0f, false);
        } else if(T < 4.0f) {
            if (T > 3.0f) {
                SnowballRb.AddForce(Vector3.forward * -100);
            }
            transform.localPosition = origin + 0.05f * (T-2) * (Vector3)(Random.insideUnitCircle);
        } else if (T < 5.0f) {
            transform.localPosition = origin + 0.1f * (Vector3)(Random.insideUnitCircle);
        } else if (T < 7.0f) {
            if (!board)
            {
                Invoke("InvokeShowBoard", 2.0f);
                board = true;
            }
            transform.localPosition = origin + 0.05f * (7-T) * (Vector3)(Random.insideUnitCircle);
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
