using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public Image Img;

    void Start() 
    {
        Img.gameObject.SetActive(false);
    }
    public void OnClickStartButton(string level)
    {
        Img.gameObject.SetActive(true);
        Img.CrossFadeAlpha(10, 1.0f, false);
        
        if (level == "Easy")
        {
            Invoke("LoadIntro", 1.0f);
        }
        else if (level == "Hard")
        {
            Invoke("LoadIntro", 1.0f);
        }
    }

    public void LoadIntro() {
        SceneManager.LoadScene("Intro");
    }
}
