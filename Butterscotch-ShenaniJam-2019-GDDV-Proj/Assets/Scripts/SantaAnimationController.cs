﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SantaAnimationController : NetworkBehaviour
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
        if (hasAuthority)
        {
            if (!variablesAsssigned)
            {
                anim = GetComponent<Animator>();
                moveCont = GetComponent<CharacterController>();
                variablesAsssigned = true;
            }
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
                anim.SetBool("Attack", true);
                anim.SetFloat("AttackRandom", Random.Range(0, 1));
                Invoke("EndAttack", 0.5f);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HUDManager>().pausePanel.SetActive(!GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HUDManager>().pausePanel.activeSelf);
            }

            if (GameObject.FindGameObjectsWithTag("Gift").Length == 0)
            {
                GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HUDManager>().deathPanel.SetActive(true);
            }

            if (GameObject.FindGameObjectsWithTag("Players").Length != 0)
            {
                int i = GameObject.FindGameObjectsWithTag("Players").Length;

                foreach (var player in GameObject.FindGameObjectsWithTag("Players"))
                {
                    if (player.GetComponent<PlayerAnimationController>().Dead)
                    {
                        i--;
                    }
                    
                }

                if (i == 0)
                {
                    GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HUDManager>().winPlayerPanel.SetActive(true);
                }
            }



            anim.SetFloat("Horizontal_f", Input.GetAxis("Horizontal"));
            anim.SetFloat("Vertical_f", Input.GetAxis("Vertical"));
        }

    }

    void EndAttack()
    {
        anim.SetBool("Attack", false);
    }
}
