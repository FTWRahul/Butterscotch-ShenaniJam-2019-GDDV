using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI playerCount;
    public TextMeshProUGUI giftsRemaining;


    private void Update()
    {
        playerCount.text = (GameObject.FindGameObjectsWithTag("Player").Length + 1).ToString();
        giftsRemaining.text = GameObject.FindGameObjectsWithTag("Gift").Length.ToString();
    }
}
