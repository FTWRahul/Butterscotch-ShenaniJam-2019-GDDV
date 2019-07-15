using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaAnimationController : MonoBehaviour
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

        if (moveCont.isGrounded)
        {
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Jump", true);
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetBool("Attack",true);
            anim.SetFloat("AttackRandom", Random.Range(0, 1));
            Invoke("EndAttack", 0.5f);
        }

        anim.SetFloat("Horizontal_f", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical_f", Input.GetAxis("Vertical"));
    }

    void EndAttack()
    {
        anim.SetBool("Attack", false);
    }
}
