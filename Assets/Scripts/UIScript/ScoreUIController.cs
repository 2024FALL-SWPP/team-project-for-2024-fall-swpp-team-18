using System;
using System.Collections;
using TMPro; // TextMeshPro 사용 시 필요
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스

public class ScoreUIController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gradeText;
    public TextMeshProUGUI studentText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI lectureText;
    public TextMeshProUGUI professorText;

    public float animationDuration = 1.0f; // 점수 애니메이션 지속 시간

    void Start()
    {
        StartCoroutine(ShowTextsSequentially());
    }

    IEnumerator ShowTextsSequentially()
    {
        // 2. Grade
        yield return StartCoroutine(
            AnimateValue(gradeText, "grade\n", 0, GameManager.instance.grade, false)
        );

        // 5. Lecture
        yield return StartCoroutine(
            AnimateValue(lectureText, "lecture\n", 0, GameManager.instance.gradeNum, true)
        );

        // 3. Students
        yield return StartCoroutine(
            AnimateValue(studentText, "rescued students\n", 0, GameManager.instance.student, true)
        );

        // 4. Time
        yield return StartCoroutine(
            AnimateValue(timeText, "time\n", 0, GameManager.instance.time, false)
        );

        // 6. Professor
        yield return StartCoroutine(
            AnimateValue(professorText, "professor\n", 0, GameManager.instance.professor, true)
        );
        // 1. Score
        yield return StartCoroutine(
            AnimateValue(scoreText, "Score : ", 0, GameManager.instance.total, true)
        );
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Main");
    }

    IEnumerator AnimateValue(
        TextMeshProUGUI targetText,
        string prefix,
        float startValue,
        float endValue,
        bool isInt
    )
    {
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentValue = Mathf.Lerp(startValue, endValue, elapsedTime / animationDuration);
            if (!isInt)
                targetText.text = prefix + (Mathf.Round(currentValue * 10) / 10).ToString();
            else
                targetText.text = prefix + Mathf.RoundToInt(currentValue).ToString();
            yield return null;
        }

        // 최종 값 설정
        if (!isInt)
            targetText.text = prefix + (Mathf.Round(endValue * 10) / 10).ToString();
        else
            targetText.text = prefix + Mathf.RoundToInt(endValue).ToString();
    }
}
