using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
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

        //if(isDead)
        //{
        //anim.SetTrigger(2);
        //}

        //if(recived hit)
        //{
        //    anim.SetTrigger(3);
        //}

        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger(4);
            anim.SetFloat(7, Random.Range(0, 1));
        }

        //anim.SetFloat(5,horizontal(-1,1));
        //anim.SetFloat(6,vertical(-1,1));
    }
}
