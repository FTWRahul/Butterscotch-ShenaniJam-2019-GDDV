using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerIdentity : NetworkBehaviour
{
    public GameObject playerPersonPrefab;
    public GameObject santaPersonPrefab;
    public bool isSanta;

    void Start()
    {
        if (hasAuthority)
        {
            if (NetworkServer.connections.Count == 1)
            {
                CmdSpawnMyPlayer();
            }
            else
            {
                CmdSpawnMyPlayer();
            }
        }
    }


    [Command]
    void CmdSpawnMyPlayer()
    {
        GameObject go = Instantiate(playerPersonPrefab);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    [Command]
    void CmdSpawnMySanta()
    {
        GameObject go = Instantiate(santaPersonPrefab);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
}
