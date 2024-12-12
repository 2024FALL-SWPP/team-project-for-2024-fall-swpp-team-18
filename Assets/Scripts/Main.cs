using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public void OnClickStartButton(string level)
    {
        if (level == "Easy")
        {
            SceneManager.LoadScene("Jinsik");
        }
        else if (level == "Hard")
        {
            SceneManager.LoadScene("Jinsik");
        }
    }
}
