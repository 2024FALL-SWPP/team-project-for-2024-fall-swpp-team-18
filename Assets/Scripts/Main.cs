using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public void OnClickStartButton(string level)
    {
        if (level == "Easy")
        {
            SceneManager.LoadScene("MainScene");
        }
        else if (level == "Hard")
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
