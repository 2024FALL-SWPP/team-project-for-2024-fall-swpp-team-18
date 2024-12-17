using UnityEngine;
using UnityEngine.UI;

public class FireballUIController : MonoBehaviour
{
    private Image targetImage; // 색상을 변경할 Image 컴포넌트
    private GameObject scoreManager;
    private ScoreManager scoreManagerScript;
    private bool isFireball = false; // 상태를 나타냄
    

    void Start()
    {

        scoreManager = GameObject.Find("ScoreManager");
        scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
        // Image 컴포넌트를 가져옴
        targetImage = GetComponent<Image>();
        if (targetImage == null)
        {
            Debug.LogError("No Image component found on this GameObject!");
        }
        UpdateisFireball(scoreManagerScript.fireball);
    }

    void Update()
    {   
        if((scoreManagerScript.fireball > 0 && isFireball == false ) || (scoreManagerScript.fireball == 0 && isFireball == true ))
        UpdateisFireball(scoreManagerScript.fireball);
        if (targetImage == null) return;

        // isFireball 값에 따라 색상 결정
        string hexColor = isFireball ? "#FFFFFF" : "#393939";
        Color newColor;

        if (ColorUtility.TryParseHtmlString(hexColor, out newColor))
        {
            newColor.a = 1f; // 알파 값 1로 설정
            targetImage.color = newColor; // 색상 적용
        }
        else
        {
            Debug.LogError($"Invalid Hex Color: {hexColor}");
        }
    }
    public void UpdateisFireball(int fireball){
        if(fireball == 0){
            isFireball = false;
        }
        else{
            isFireball = true;
        }

    }
}
