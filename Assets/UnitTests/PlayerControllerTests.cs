using NUnit.Framework;
using UnityEngine;

public class PlayerControllerTests
{
    private PlayerController playerController;

    [SetUp]
    public void SetUp()
    {
        // 테스트를 위한 PlayerController 인스턴스 생성
        playerController = new PlayerController();
    }

    [Test]
    public void CalculateTargetRotation_WhenOnJumpIsTrue_ReturnsParentRotation()
    {
        // Arrange
        var currentRotation = Quaternion.identity;
        var parentRotation = Quaternion.Euler(0, 90, 0);
        var parentForward = Vector3.forward;
        var speed = 100f;
        var deltaTime = 0.02f;

        // Act
        var result = playerController.CalculateTargetRotation(
            currentRotation,
            parentRotation,
            parentForward,
            isQPressed: false,
            isEPressed: false,
            onJump: true,
            speed,
            deltaTime
        );

        // Assert
        Assert.AreEqual(parentRotation, result);
    }

    [Test]
    public void CalculateTargetRotation_WhenQKeyPressed_ReturnsCorrectRotation()
    {
        // Arrange
        var currentRotation = Quaternion.identity;
        var parentRotation = Quaternion.Euler(0, 90, 0);
        var parentForward = Vector3.forward;
        var speed = 100f;
        var deltaTime = 0.02f;

        // Act
        var result = playerController.CalculateTargetRotation(
            currentRotation,
            parentRotation,
            parentForward,
            isQPressed: true,
            isEPressed: false,
            onJump: false,
            speed,
            deltaTime
        );

        // Assert
        var expectedRotation = Quaternion.RotateTowards(
            currentRotation,
            Quaternion.Euler(parentForward * 15) * parentRotation,
            speed * deltaTime
        );

        Assert.AreEqual(expectedRotation, result);
    }

    [Test]
    public void CalculateTargetRotation_WhenEKeyPressed_ReturnsCorrectRotation()
    {
        // Arrange
        var currentRotation = Quaternion.identity;
        var parentRotation = Quaternion.Euler(0, 90, 0);
        var parentForward = Vector3.forward;
        var speed = 100f;
        var deltaTime = 0.02f;

        // Act
        var result = playerController.CalculateTargetRotation(
            currentRotation,
            parentRotation,
            parentForward,
            isQPressed: false,
            isEPressed: true,
            onJump: false,
            speed,
            deltaTime
        );

        // Assert
        var expectedRotation = Quaternion.RotateTowards(
            currentRotation,
            Quaternion.Euler(parentForward * -15) * parentRotation,
            speed * deltaTime
        );

        Assert.AreEqual(expectedRotation, result);
    }

    [Test]
    public void CalculateTargetRotation_WhenNoKeyPressed_ReturnsParentRotation()
    {
        // Arrange
        var currentRotation = Quaternion.identity;
        var parentRotation = Quaternion.Euler(0, 90, 0);
        var parentForward = Vector3.forward;
        var speed = 100f;
        var deltaTime = 0.02f;

        // Act
        var result = playerController.CalculateTargetRotation(
            currentRotation,
            parentRotation,
            parentForward,
            isQPressed: false,
            isEPressed: false,
            onJump: false,
            speed,
            deltaTime
        );

        // Assert
        Assert.AreEqual(parentRotation, result);
    }
}
