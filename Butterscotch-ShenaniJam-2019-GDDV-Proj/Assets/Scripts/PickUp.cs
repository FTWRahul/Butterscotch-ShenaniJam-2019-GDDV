using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUp : MonoBehaviour
{
    Vector2 dotPos = new Vector2(625, 320);

    bool isChecking;

    int checkAmount;

    public GameObject hud;
    GameObject skillcheck;
    public RectTransform sliderBackground;
    public RectTransform sliderDot;
    public RectTransform sliderArea;

    public TextMeshProUGUI msg;

    int AreaW = 30;
    float progress;
    int dir = 1;
    int speedSlider = 4;

    GameObject gift;

    private void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD");
        skillcheck = hud.transform.GetChild(1).gameObject;
        sliderBackground = skillcheck.transform.GetChild(0).GetComponent<RectTransform>();
        sliderArea = skillcheck.transform.GetChild(1).GetComponent<RectTransform>();
        sliderDot = skillcheck.transform.GetChild(2).GetComponent<RectTransform>();
        msg = hud.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(dotPos);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.collider.tag == "Gift")
            {
                msg.gameObject.SetActive(true);
                msg.text = "F for kick";
            }
            else
            {
                msg.gameObject.SetActive(false);
            }
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
                        sliderArea.anchoredPosition = new Vector2(Random.Range(30, 320), 0);
                        Debug.Log("Only " + checkAmount + " left");
                    }
                    else
                    {
                        Debug.Log("None");
                        speedSlider = 0;
                        //close slider
                        //make sound
                    }

                }
                else
                {
                    //closeslider
                    //gift ==null
                    speedSlider = 0;
                    isChecking = false;
                }

            }
            else
            {
                if (hit.collider.tag == "Gift")
                {
                    gift = hit.collider.gameObject;
                }

                isChecking = true;

                sliderArea.anchoredPosition = new Vector2(Random.Range(30, 320), 0);
                checkAmount = Random.Range(3, 6);
            }
        }


        if (skillcheck.activeSelf == true)
        {
            if (sliderDot.anchoredPosition.x <= 5)
            {
                dir = 1;
            }
            else if (sliderDot.anchoredPosition.x >= 340)
            {
                dir = -1;
            }
            sliderDot.anchoredPosition += new Vector2(dir, 0) * speedSlider;
        }

        if (checkAmount <= 0 && isChecking)
        {
            Debug.Log("Win");
            speedSlider = 0;
            //gift==null
            //destroy gift
            //add amount
        }

    }
}

