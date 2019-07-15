using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnGifts : NetworkBehaviour
{
    public GameObject[] spawnPositions;
    public GameObject giftsRoot;

    public List<GameObject> gifts;

    int amount = 10;

    private void Start()
    {
        GetSpawnArea();
    }

    void GetSpawnArea()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            GetPointOnMesh(spawnPositions[i].gameObject.transform.position);
        }
    }

    void GetPointOnMesh(Vector3 pos)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 PointRay = pos + Random.insideUnitSphere * 15;

            RaycastHit hit;

            if (Physics.Raycast(PointRay + new Vector3(0, 100.0f, 0), Vector3.down, out hit, 200.0f))
            {
                if (hit.collider.tag == "Ground")
                {
                    CmdSpawnGift(hit.point);
                }
            }
        }
    }

    [Command]
    void CmdSpawnGift(Vector3 pos)
    {
        GameObject go = Instantiate(gifts[Random.Range(0, gifts.Count)], pos, Quaternion.Euler(0, Random.Range(0, 360), 0), giftsRoot.transform);
        NetworkServer.Spawn(go);
    }
}
