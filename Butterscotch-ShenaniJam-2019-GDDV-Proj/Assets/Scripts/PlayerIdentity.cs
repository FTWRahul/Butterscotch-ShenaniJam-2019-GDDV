using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerIdentity : NetworkBehaviour
{
    public GameObject playerPersonPrefab;

    void Start()
    {
        if (hasAuthority)
        {
            CmdSpawnMyUnit();
        }
    }


    [Command]
    void CmdSpawnMyUnit()
    {
        GameObject go = Instantiate(playerPersonPrefab);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
}
