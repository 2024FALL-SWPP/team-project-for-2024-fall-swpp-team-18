using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        float anim_int = Random.value;
        if (anim_int < 0.3f)
        {
            animator.SetInteger("pose_int", 1);
        }
        else if (anim_int < 0.6f)
        {
            animator.SetInteger("pose_int", 2);
        }
        else
        {
            animator.SetInteger("pose_int", 3);
        }
    }

    // Update is called once per frame
    void Update() { }
}
