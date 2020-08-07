﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.XR;

public class MyNetworkManager : NetworkManager {

    private MatchInfoSnapshot match;

    private void Start() {
        if (!XRDevice.isPresent) {
            playerPrefab = spawnPrefabs.First();
            GameObject.Find("EventSystem").SetActive(true);
            GameObject.Find("VRInputModule").SetActive(false);
        } else {
            GameObject.Find("EventSystem").SetActive(false);
            GameObject.Find("VRInputModule").SetActive(true);
        }
    }

    public void StartMatch() {
        StartMatchMaker();
        matchMaker.CreateMatch(matchName, matchSize, true, "", "", "", 0, 0, OnMatchCreate);
    }

    public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList) {
        matches = matchList;
        base.OnMatchList(success, extendedInfo, matchList);
    }

    public void JoinMatch() {
        StartMatchMaker();
        matchMaker.ListMatches(0, 1, "", true, 0, 0, OnMatchList);
        // vielleicht eine Schleife, solange bis die Coroutine beendet ist
        // oder eine weitere Coroutine, in der ListMatches aufgerufen wird. 
        // dort wird dann gewartet bis ListMatches fertig ist.
        foreach (MatchInfoSnapshot m in matches) {
            if (m.name == matchName) {
                match = m;
            }
        }
        matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, OnMatchJoined);
    }

    public void EndGame() {
        Application.Quit();
    }
}
