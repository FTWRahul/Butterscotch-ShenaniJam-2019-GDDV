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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Attack");
            anim.SetFloat("AttackRandom", Random.Range(0, 1));
        }

        anim.SetFloat("Horizontal_f", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical_f", Input.GetAxis("Vertical"));
    }
}
