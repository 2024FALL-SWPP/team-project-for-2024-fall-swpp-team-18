using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest, Timeout(300000)]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        bool isEscPressed = false;
        SceneManager.LoadScene("Main");
        yield return null;

        GameObject gameManager = GameObject.Find("GameManager");
        Assert.IsNotNull(gameManager, "GameManager 오브젝트를 찾을 수 없습니다.");

        // 2. GameManager 컴포넌트 가져오기
        GameManager gameManagerScript = gameManager.GetComponent<GameManager>();
        Assert.IsNotNull(gameManagerScript, "GameManager 컴포넌트를 찾을 수 없습니다.");

        // 3. 값 변경
        gameManagerScript.isTest = true; // 원하는 값을 설정
        Assert.AreEqual(true, gameManagerScript.isTest, "GameManager 값이 변경되지 않았습니다.");

        var startButton = GameObject.Find("Easy Button");
        Assert.IsNotNull(startButton, "StartButton이 존재하지 않습니다.");
        startButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        yield return new WaitForSeconds(1); // 씬 전환 대기

        Assert.AreEqual(
            "Intro",
            SceneManager.GetActiveScene().name,
            "인트로 씬이 로드되지 않았습니다."
        );
        yield return new WaitForSeconds(4);

        var skipButton = GameObject.Find("Skip");
        Assert.IsNotNull(skipButton, "SkipButton이 존재하지 않습니다.");
        skipButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        yield return new WaitForSeconds(8); // 씬 전환 대기

        Assert.AreEqual(
            "MainScene",
            SceneManager.GetActiveScene().name,
            "게임 씬이 로드되지 않았습니다."
        );
        yield return new WaitForSeconds(12);

        yield return null; // 씬 전환 대기

        Assert.IsNotNull(gameManager, "GameManager가 존재하지 않습니다.");
        var clearState = (gameManager.GetComponent<GameManager>().getState() == State.GameClear);
        Assert.AreEqual(false, clearState, "게임을 클리어했습니다.");
        var overState = (gameManager.GetComponent<GameManager>().getState() == State.GameOver);
        Assert.AreEqual(false, overState, "게임이 오버되었습니다.");
        yield return new WaitForSeconds(200);

        yield return null;
    }
}
