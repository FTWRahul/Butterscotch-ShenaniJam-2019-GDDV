using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerIdentity : NetworkBehaviour
{
    public GameObject playerPersonPrefab;
    public GameObject playerPersona;
    public GameObject cameraPrefab;


    // Start is called before the first frame update
    void Start()
    {
        if (hasAuthority)
        {
            CmdSpawnMyUnit();
            GameObject go = Instantiate(cameraPrefab, playerPersona.transform);
            playerPersona.GetComponent<PlayerMove>().cameraTransform = go.transform;
        }
    }


    [Command]
    void CmdSpawnMyUnit()
    {
        // We are guaranteed to be on the server right now.
        GameObject go = Instantiate(playerPersonPrefab);

        playerPersona = go;

        //go.GetComponent<NetworkIdentity>().AssignClientAuthority( connectionToClient );

        // Now that the object exists on the server, propagate it to all
        // the clients (and also wire up the NetworkIdentity)
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
}
