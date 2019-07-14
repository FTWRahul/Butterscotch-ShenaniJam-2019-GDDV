using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLookAt : MonoBehaviour
{

    public bool isRealPlyaer;
    public bool StartLookAt;

    public Transform target;

    // Update is called once per frame
    void Update()
    {
        if(!isRealPlyaer && StartLookAt)
        {
            transform.LookAt(target);
        }
    }
}
