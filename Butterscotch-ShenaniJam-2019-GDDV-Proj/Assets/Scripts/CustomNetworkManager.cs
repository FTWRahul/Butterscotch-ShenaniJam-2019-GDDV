using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class CustomNetworkManager : NetworkManager
{
    public string playerName;

    public void StartHosting()
    {
        StartMatchMaker();
        matchMaker.CreateMatch(playerName, 5, true, "", "", "", 0, 0, OnMatchCreated);      
    }

    private void OnMatchCreated(bool success, string extendedInfo, MatchInfo responseData)
    {
        base.StartHost();
    }

    public void RefreshMatches()
    {
        if(matchMaker == null)
        {
            StartMatchMaker();
        }

        matchMaker.ListMatches(0,10, "", true, 0,0, HandleListMatchesComplet);
    }

    private void HandleListMatchesComplet(bool success, string extendedInfo, List<MatchInfoSnapshot> responseData)
    {
        if(success)
        {
            AviliableMatchesList.HandleNewMatchList(responseData);
        }
        else
        {
            //No matches found
        }
    }
}
