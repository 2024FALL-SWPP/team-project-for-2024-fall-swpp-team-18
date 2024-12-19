using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ScoreManagerTests
{
    private ScoreManager scoreManager;

    [SetUp]
    public void SetUp()
    {
        var gmObj = new GameObject("GameManager");
        var gm = gmObj.AddComponent<GameManager>();
        GameManager.instance = gm;
        gm.isTest = true; // 테스트 모드

        var scoreObj = new GameObject("ScoreManager");
        scoreManager = scoreObj.AddComponent<ScoreManager>();

        // SFXController 인스턴스 생성
        var sfxObj = new GameObject("SFXController");
        sfxObj.AddComponent<SFXController>();

        // BackgroundMusicController 인스턴스 생성
        var bgmObj = new GameObject("BackgroundMusicController");
        bgmObj.AddComponent<BackgroundMusicController>();
    }


    [Test]
    public void IncreaseHeartTest()
    {
        // heart의 초기값을 확인 (기본값이 3이라 가정)
        Assert.AreEqual(3, scoreManager.heart);

        // IncreaseHeart를 호출
        scoreManager.IncreaseHeart();
        Assert.AreEqual(4, scoreManager.heart);

        // heart가 5를 넘지 않는지 확인
        scoreManager.heart = 5;
        scoreManager.IncreaseHeart();
        Assert.AreEqual(5, scoreManager.heart);
    }

    [Test]
    public void DecreaseHeartTest()
    {
        // heart의 초기값 확인
        scoreManager.heart = 3;
        scoreManager.DecreaseHeart();
        Assert.AreEqual(2, scoreManager.heart);

        // heart가 1일 때 DecreaseHeart 호출 시 게임 오버 호출 로직이 실행되는지 테스트
        // 이 경우, 실제로 GameManager를 mocking하거나, GameManager.Instance.HandleGameOver를 호출했는지 확인할 수 있어야 합니다.
        // 간단히 heart 값 변화만 체크(비즈니스 로직 검증)하려면 heart == 0일 때를 확인합니다.
        scoreManager.heart = 1;
        // GameManager에 대한 Mocking이나 Stub 필요할 수 있음.
        // 일단 Heart 감소 후 heart == 0으로 만들고 추후 게임오버 메소드 호출 테스트는 별도로 진행.
        scoreManager.DecreaseHeart();
        Assert.AreEqual(0, scoreManager.heart); 
        // 여기서 GameManager의 함수가 호출됐는지 검증하려면 GameManager 자체를 Mock 처리하거나
        // 게임오버 로직을 분리하여 테스트할 필요가 있습니다.
    }

    [Test]
    public void IncreaseGradeTest()
    {
        // 초기값 설정
        scoreManager.grade = 0f;
        scoreManager.gradeNum = 0;

        // 첫번 째 호출: grade = ((0 * 0) + 95) / (0+1) = 95
        scoreManager.IncreaseGrade(95f);
        Assert.AreEqual(95f, scoreManager.grade);
        Assert.AreEqual(1, scoreManager.gradeNum);

        // 두번 째 호출: grade = ((95 * 1) + 85) / (1+1) = (95 + 85) / 2 = 180/2 = 90
        scoreManager.IncreaseGrade(85f);
        Assert.AreEqual(90f, scoreManager.grade);
        Assert.AreEqual(2, scoreManager.gradeNum);

        // 소수점 반올림 테스트 (already done by Mathf.Round(grade*100)/100f)
        // 예를 들어 90.1234같은 값에 대한 처리도 테스트 가능
    }

    [Test]
    public void CalculateTotalTest()
    {
        // 초기 상황 설정
        scoreManager.grade = 90f;
        scoreManager.gradeNum = 2; // 예: 2개의 점수 평균
        scoreManager.student = 5;
        scoreManager.playTime = 10f;
        scoreManager.professor = 1;
        scoreManager.total = 0;

        // GameManager.instance.isGameClear = false 인 상황 가정
        GameManager.instance.isGameClear = false;
        int total = scoreManager.CalculateTotal();
        // total 계산 로직:
        // total = (int)(grade * 100 * gradeNum) + student * 100 + (int)(playTime * 50);
        //      = (int)(90 * 100 * 2) + 5*100 + (int)(10*50)
        //      = (int)(18000) + 500 + 500
        //      = 19000
        Assert.AreEqual(19000, total);

        // GameManager.instance.isGameClear = true 인 상황 가정
        GameManager.instance.isGameClear = true;
        // total = ((total + (int)((200 - playTime)*500))*(professor+1));
        // 위 테스트에서는 total이 계속 누적되므로 먼저 total을 초기화 하거나 새로 설정해야 할 수도 있습니다.
        scoreManager.total = 0;
        scoreManager.playTime = 10f;
        scoreManager.professor = 1;
        // (200 - 10)*500 = 190 * 500 = 95000
        // total = (0 + 95000) * (1+1) = 95000 * 2 = 190000
        total = scoreManager.CalculateTotal();
        Assert.AreEqual(190000, total);
    }
}
