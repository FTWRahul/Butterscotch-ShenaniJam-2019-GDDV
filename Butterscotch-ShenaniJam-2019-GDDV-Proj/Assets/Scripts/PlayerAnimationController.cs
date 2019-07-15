using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAnimationController : NetworkBehaviour
{
    Animator anim;

    CharacterController moveCont;

    bool variablesAsssigned;

    [SyncVar]
    public int Health = 5;
    [SyncVar]
    public bool Dead;

    private void Start()
    {
        anim = GetComponent<Animator>();
        moveCont = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(hasAuthority)
        {
            if (!variablesAsssigned)
            {
                anim = GetComponent<Animator>();
                moveCont = GetComponent<CharacterController>();
                variablesAsssigned = true;
                GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HUDManager>().health.transform.parent.gameObject.SetActive(true);
            }
            GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HUDManager>().health.text = Health.ToString();
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
                anim.SetBool("Kick",true);
                anim.SetFloat("KickRandom", Random.value);

                Invoke("EndKick", 0.5f);
            }

            anim.SetFloat("Horizontal_f", Input.GetAxis("Horizontal"));
            anim.SetFloat("Vertical_f", Input.GetAxis("Vertical"));
        }
        
    }

    void EndKick()
    {
        anim.SetBool("Kick", false);
    }
    void EndHit()
    {
        anim.SetBool("GetHit", false);
    }

    [Client]
    public void TakeDamage()
    {
        if(Health > 1 && !Dead)
        {
            Debug.Log("Health Remaining : "+ Health);
            Health--;
            anim.SetBool("GetHit", true);
            Invoke("EndHit", 0.5f);
        }
        else
        {
            Dead = true;
            PlayerDeath();
            GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HUDManager>().deathPanel.SetActive(true);
        }
    }

    public void PlayerDeath()
    {
        anim.SetBool("Death", true);
        GetComponent<PlayerMove>().enabled = false;
    }
}
