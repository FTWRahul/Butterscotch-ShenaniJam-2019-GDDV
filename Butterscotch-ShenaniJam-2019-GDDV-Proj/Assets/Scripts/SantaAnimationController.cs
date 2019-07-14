using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaAnimationController : MonoBehaviour
{
    Animator anim;

    CharacterController moveCont;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        moveCont = GetComponentInParent<CharacterController>();
    }

    private void Update()
    {
        anim.SetBool(0, moveCont.isGrounded);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger(1);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger(2);
            anim.SetFloat(5, Random.Range(0, 1));
        }

        //anim.SetFloat(3,horizontal(-1,1));
        //anim.SetFloat(4,vertical(-1,1));
    }
}
