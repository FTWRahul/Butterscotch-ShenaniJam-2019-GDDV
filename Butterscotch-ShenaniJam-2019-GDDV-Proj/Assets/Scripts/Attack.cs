using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    int Range = 2;
    Transform axePoint;
    Camera cam;

    public GameObject hud;
    public RectTransform dotPos;

    bool isAttacking;

    private void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD");
        dotPos = hud.transform.GetChild(0).GetComponent<RectTransform>();
    }

    void Update()
    {
        cam = GetComponentInChildren<Camera>();
        if (cam == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            Invoke("axeRayShoot", 0.5f);
            isAttacking = true;
        }
    }

    void axeRayShoot()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(dotPos.position);
        if (Physics.Raycast(ray, out hit, Range))
        {
            if (hit.collider.tag == "Player")
            {
                //hit.collider.gameObject => damage
            }
        }
        Invoke("EndAttack", 0.5f);
    }

    void EndAttack()
    {
        isAttacking = false;
    }


}
