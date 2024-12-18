using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Image Img;
    public GameObject Background;
    private Vector2 initPos;
    private float resetPosX = 3000.0f,
        speed = 200.0f;

    void Start()
    {
        initPos = Background.GetComponent<RectTransform>().anchoredPosition;
        Img.CrossFadeAlpha(0, 1.0f, false);
        Invoke("RemoveBlackScreen", 1.0f);
    }

    void Update()
    {
        Background.GetComponent<RectTransform>().anchoredPosition -= new Vector2(
            speed * Time.deltaTime,
            0
        );
        Vector2 curPos = Background.GetComponent<RectTransform>().anchoredPosition;
        if (initPos.x - curPos.x >= resetPosX)
        {
            Background.GetComponent<RectTransform>().anchoredPosition = initPos;
        }
    }

    public void OnClickStartButton(string level)
    {
        Img.gameObject.SetActive(true);
        Img.CrossFadeAlpha(1.0f, 1.0f, false);

        if (level == "Easy")
        {
            GameManager.instance.isEasy = true;
            Invoke("LoadIntro", 1.0f);
        }
        else if (level == "Hard")
        {
            GameManager.instance.isEasy = false;
            Invoke("LoadIntro", 1.0f);
        }
    }

    public void LoadIntro()
    {
        SceneManager.LoadScene("Intro");
    }

    private void RemoveBlackScreen()
    {
        Img.gameObject.SetActive(false);
    }
}
