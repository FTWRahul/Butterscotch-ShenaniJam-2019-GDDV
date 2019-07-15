using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class PickUp : MonoBehaviour
{

    bool isChecking;

    int checkAmount;

    public GameObject hud;
    GameObject skillcheck;
    public RectTransform dotPos;
    public RectTransform sliderBackground;
    public RectTransform sliderDot;
    public RectTransform sliderArea;

    public TextMeshProUGUI msg;

    int AreaW = 30;
    float progress;
    int dir = 1;
    int speedSlider = 5;

    GameObject gift;

    public Camera cam;

    private void Start()
    {
        
        hud = GameObject.FindGameObjectWithTag("HUD");
        dotPos = hud.transform.GetChild(0).GetComponent<RectTransform>();
        skillcheck = hud.transform.GetChild(1).gameObject;
        sliderBackground = skillcheck.transform.GetChild(0).GetComponent<RectTransform>();
        sliderArea = skillcheck.transform.GetChild(1).GetComponent<RectTransform>();
        sliderDot = skillcheck.transform.GetChild(2).GetComponent<RectTransform>();
        msg = hud.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        msg.text = "F for kick";

    }

    private void Update()
    {
        cam = GetComponentInChildren<Camera>();

        if (cam == null)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(dotPos.position);
        if (Physics.Raycast(ray, out hit, 5.0f))
        {
            if (hit.collider.tag == "Gift")
            {
                msg.gameObject.SetActive(true);
            }
            else
            {
                msg.gameObject.SetActive(false);
                CloseSkillCheck();
            }
        }
        else
        {
            msg.gameObject.SetActive(false);
            CloseSkillCheck();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isChecking)
            {
                if (hit.collider.gameObject == gift)
                {
                    float sliderDotPos = sliderDot.anchoredPosition.x;
                    if (sliderDotPos > sliderArea.anchoredPosition.x - AreaW && sliderDotPos < sliderArea.anchoredPosition.x + AreaW)
                    {
                        checkAmount -= 1;
                        speedSlider += 1;
                        sliderArea.anchoredPosition = new Vector2(Random.Range(-95, 190), 0);
                    }
                    else
                    {
                        CloseSkillCheck();
                        //make sound
                    }
                }
                else
                {
                    CloseSkillCheck();
                }
            }
            else
            {
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Gift")
                    {
                        gift = hit.collider.gameObject;
                        ActivateSkillCheck();
                    }
                }
            }
        }


        if (skillcheck.activeSelf == true)
        {
            if (sliderDot.anchoredPosition.x <= -110)
            {
                dir = 1;
            }
            else if (sliderDot.anchoredPosition.x >= 215)
            {
                dir = -1;
            }
            sliderDot.anchoredPosition += new Vector2(dir, 0) * speedSlider;
        }

        if (checkAmount <= 0 && isChecking)
        {
            if (GetComponent<GiftDestroyer>() != null)
            {
                GetComponent<GiftDestroyer>().CmdDestroyGift(gift);
            }
            CloseSkillCheck();

        }

        void CloseSkillCheck()
        {
            isChecking = false;
            skillcheck.SetActive(false);
            gift = null;
        }

        void ActivateSkillCheck()
        {
            isChecking = true;
            skillcheck.SetActive(true);
            sliderArea.anchoredPosition = new Vector2(Random.Range(-95, 190), 0);
            checkAmount = Random.Range(3, 5);
            speedSlider = 5;
        }

    }
}

