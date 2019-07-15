using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAnimationController : NetworkBehaviour
{
    Animator anim;

    CharacterController moveCont;

    bool variablesAsssigned;

    private void Start()
    {
        anim = GetComponent<Animator>();
        moveCont = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(hasAuthority)
        {
            if(!variablesAsssigned)
            {
                anim = GetComponent<Animator>();
                moveCont = GetComponent<CharacterController>();
                variablesAsssigned = true;
            }
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

            anim.SetFloat("Horizontal_f", Input.GetAxis("Horizontal"));
            anim.SetFloat("Vertical_f", Input.GetAxis("Vertical"));
        }
        
    }
}
