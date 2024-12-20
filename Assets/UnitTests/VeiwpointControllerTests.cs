using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ViewpointControllerTests
{
    private GameObject testObject;
    private ViewpointController controller;

    [SetUp]
    public void SetUp()
    {
        // 테스트용 객체 및 ViewpointController 추가
        testObject = new GameObject();
        var parentObject = new GameObject(); // 부모 GameObject 생성
        testObject.transform.parent = parentObject.transform; // 부모 설정
        controller = testObject.AddComponent<ViewpointController>();
        controller.enabled = false; // Update 호출 방지
    }

    [Test]
    public void HandleMouseRotation_WithinBounds()
    {
        // Arrange
        controller.enabled = true;
        float initialMouseX = 0f;

        // Simulate LeftShift being held
        InputTestHelper.SetKey(KeyCode.LeftShift, true);
        InputTestHelper.SetAxis("Mouse X", 10f);

        // Act
        controller.Update(); // 한 번의 Update 호출

        // Assert
        Assert.AreEqual(
            initialMouseX + controller.mouseSpeed * 10f,
            controller.transform.localEulerAngles.y,
            $"Expected mouse Y to be {initialMouseX + controller.mouseSpeed * 10f}, but was {controller.transform.localEulerAngles.y}"
        );
    }

    [Test]
    public void HandleMouseRotation_ClampsMouseXWithinBounds()
    {
        // Arrange
        InputTestHelper.SetKey(KeyCode.LeftShift, true);
        InputTestHelper.SetAxis("Mouse X", 200f); // 큰 값으로 설정 (최대값 테스트)

        // Act
        controller.Update();

        // Assert
        // mouseX가 설정된 maxMouseX 값을 초과하지 않는지 확인
        float expectedMouseX = controller.maxMouseX; // maxMouseX는 150f로 설정됨
        Assert.AreEqual(
            expectedMouseX,
            controller.transform.localEulerAngles.y,
            0.01f,
            $"Expected mouse X to be {expectedMouseX}, but was {controller.transform.localEulerAngles.y}"
        );
    }

    [Test]
    public void ResetToInitialRotation_WhenShiftKeyNotPressed()
    {
        // Arrange
        InputTestHelper.SetKey(KeyCode.LeftShift, false); // LeftShift를 누르지 않음

        // 초기 회전을 강제로 변경
        controller.transform.rotation = Quaternion.Euler(0, 45, 0);

        // Act
        for (int i = 0; i < 100; i++) // 100번의 Update 호출로 충분히 회전하게 함
        {
            controller.Update();
        }

        // Assert
        // 회전 값이 부모 초기 회전 값으로 복원되는지 확인
        Quaternion expectedRotation =
            Quaternion.Euler(controller.transform.parent.right * 15)
            * controller.transform.parent.rotation;
        Assert.AreEqual(
            expectedRotation.eulerAngles,
            controller.transform.rotation.eulerAngles,
            $"Expected rotation to be {expectedRotation.eulerAngles}, but was {controller.transform.rotation.eulerAngles}"
        );
    }
}
