using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator anim;

    CharacterController moveCont;    

    private void Start()
    {
        anim = GetComponent<Animator>();
        moveCont = GetComponent<CharacterController>();
    }

    private void Update()
    {
        anim.SetBool("IsGrounded", moveCont.isGrounded);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
        }

        //if(isDead)
        //{
        //anim.SetTrigger("Dead");
        //}

        //if(recived hit)
        //{
        //    anim.SetTrigger("GetHit");
        //}

        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Kick");
            anim.SetFloat("KickRandom", Random.value);
        }

        anim.SetFloat("Horizontal_f",Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical_f", Input.GetAxis("Vertical"));
    }
}
