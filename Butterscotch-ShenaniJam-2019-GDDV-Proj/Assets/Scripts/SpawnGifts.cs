using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGifts : MonoBehaviour
{
    public GameObject[] spawnPositions;
    public GameObject giftsRoot;

    public GameObject giftPrefab;

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
                    GameObject go = Instantiate(giftPrefab, hit.point, Quaternion.Euler(0, Random.Range(0, 360), 0), giftsRoot.transform);
                }
                else
                {
                    //GetPointOnMesh(pos);
                }
            }
    }
}
}
