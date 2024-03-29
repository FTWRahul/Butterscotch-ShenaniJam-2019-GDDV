﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class SantaAxeAttack : NetworkBehaviour
{
    bool variablesAssigned;
    [SerializeField]
    GameObject castPoint;
    [SerializeField]
    float castDistance;

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {
            if (!variablesAssigned)
            {
                //Assign stuff
                variablesAssigned = true;
            }

            if(Input.GetKeyDown(KeyCode.O))
            {
                CmdSphereCast();
            }
        }
    }

    public void CallServer()
    {
        CmdSphereCast();
    }

    [Command]
    public void CmdSphereCast()
    {
        RaycastHit hit;
        Collider[] colArry = Physics.OverlapSphere(castPoint.transform.position, castDistance);
        foreach (Collider col in colArry)
        {
            if (col.CompareTag("Player"))
            {
                Debug.Log("Hit!");
                string netId = col.GetComponent<NetworkIdentity>().netId.ToString();
                RpcTakeDamage(netId);
            }
        }
        
    }

    [ClientRpc]
    public void RpcTakeDamage(string inNetId)
    {
        NetworkIdentity[] netArry = FindObjectsOfType<NetworkIdentity>();
        foreach (NetworkIdentity NetIdentity in netArry)
        {
            if(NetIdentity.netId.ToString() == inNetId)
            {
                //Take Damage SyncVar?
                NetIdentity.GetComponent<PlayerAnimationController>().TakeDamage();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(castPoint.transform.position, castDistance);
    }
}
