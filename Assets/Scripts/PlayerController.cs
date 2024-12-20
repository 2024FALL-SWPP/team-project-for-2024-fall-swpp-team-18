using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 100.0f;
    private bool onJump = false;

    //public Quaternion tiltAngle;
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void FixedUpdate()
    {
        var targetRotation = CalculateTargetRotation(
            transform.rotation,
            transform.parent.rotation,
            transform.parent.forward,
            Input.GetKey(KeyCode.Q),
            Input.GetKey(KeyCode.E),
            onJump,
            speed,
            Time.deltaTime
        );
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            100 * Time.deltaTime
        );
    }

    public Quaternion CalculateTargetRotation(
        Quaternion currentRotation,
        Quaternion parentRotation,
        Vector3 parentForward,
        bool isQPressed,
        bool isEPressed,
        bool onJump,
        float speed,
        float deltaTime
    )
    {
        if (onJump)
            return parentRotation;

        if (isQPressed)
        {
            return Quaternion.RotateTowards(
                currentRotation,
                Quaternion.Euler(parentForward * 15) * parentRotation,
                speed * deltaTime
            );
        }
        else if (isEPressed)
        {
            return Quaternion.RotateTowards(
                currentRotation,
                Quaternion.Euler(parentForward * -15) * parentRotation,
                speed * deltaTime
            );
        }
        else
        {
            return parentRotation;
        }
    }
}
