using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerIdentity : NetworkBehaviour
{
    public GameObject playerPersonPrefab;
    public GameObject santaPersonPrefab;
    public bool isSanta;
    public GameObject santaSpawn;
    public GameObject playerSpawn;

    void Start()
    {
        if (hasAuthority)
        {
            if (NetworkServer.connections.Count == 1)
            {
                CmdSpawnMySanta();
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
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawns");
        GameObject go = Instantiate(playerPersonPrefab, playerSpawn.transform.GetChild(NetworkServer.connections.Count - 2).transform);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);

        Debug.Log(connectionToClient+ "IDENTITY");
        Invoke("DelayedCall", 10f);
    }

    void DelayedCall()
    {
        VoiceTesting[] voiceArry = FindObjectsOfType<VoiceTesting>();
        foreach (VoiceTesting voiceScript in voiceArry)
        {
            voiceScript.RealPlayerCall(connectionToClient, GetComponent<NetworkIdentity>().netId.ToString());
        }
    }

    [Command]
    void CmdSpawnMySanta()
    {
        santaSpawn = GameObject.FindGameObjectWithTag("SantaSpawn");
        GameObject go = Instantiate(santaPersonPrefab, santaSpawn.transform);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
}
