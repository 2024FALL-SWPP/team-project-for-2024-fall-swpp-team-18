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
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        SceneManager.LoadScene("Main");
        yield return null;

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

        var gameManager = GameObject.Find("GameManager");
        Assert.IsNotNull(gameManager, "GameManager가 존재하지 않습니다.");
        var gameStatus = gameManager.GetComponent<GameManager>().isGameClear;
        Assert.AreEqual(true, gameStatus, "게임을 클리어했습니다.");
        yield return null;
    }
}
