using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GiftDestroyer : NetworkBehaviour
{
    [Command]
    public void CmdDestroyGift(GameObject go)
    {
        NetworkServer.Destroy(go);
    }

}
