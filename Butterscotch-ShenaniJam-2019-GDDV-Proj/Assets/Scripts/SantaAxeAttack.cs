using System;
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

    [Command]
    public void CmdSphereCast()
    {
        RaycastHit hit;
        Debug.Log("CMD ATTACT FIRED ");
        if(Physics.SphereCast(castPoint.transform.position, castDistance, Vector3.forward, out hit))
        {
            Debug.Log("SphereCast Success");
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Hit!");
                string netId = hit.collider.GetComponent<NetworkIdentity>().ToString();
                RpcTakeDamage(netId);
            }
            //try
            //{
                
            //}
            //catch(Exception e)
            //{
            //    Debug.Log(e.Message);
            //}
            //finally
            //{
            //    //Do other stuff?
            //}

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

                if(!NetIdentity.GetComponent<PlayerMove>().Dead)
                {
                    NetIdentity.GetComponent<PlayerMove>().Health--;
                    Debug.Log("Health Remaining = " + NetIdentity.GetComponent<PlayerMove>().Health);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, 1);
        Gizmos.DrawWireSphere(castPoint.transform.position, castDistance);
    }
}
