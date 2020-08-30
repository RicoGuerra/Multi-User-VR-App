using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.XR;

public class MyNetworkManager : NetworkManager {

    private MatchInfoSnapshot _match;

    private void Start() {
        if (!XRDevice.isPresent) {
            //playerPrefab = spawnPrefabs.First();
        }
    }

    public void StartMatch() {
        StartCoroutine(StartM());
        //StartMatchMaker();
        //matchMaker.CreateMatch(matchName, matchSize, true, "", "", "", 0, 0, OnMatchCreate);
    }

    public IEnumerator StartM() {
        StartMatchMaker();
        matchMaker.ListMatches(0, 1, "", true, 0, 0, OnMatchList);
        yield return new WaitForSeconds(0.5f);
        if (matches.Exists(x => x.name == matchName)) {
            Debug.LogError("Match already exitst! Please press SPIEL BEITRETEN to join the match! ");
        } else {
            matchMaker.CreateMatch(matchName, matchSize, true, "", "", "", 0, 0, OnMatchCreate);
        }
    }

    public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList) {
        matches = matchList;
        base.OnMatchList(success, extendedInfo, matchList);
    }

    public void JoinMatch() {
        StartCoroutine(Join());
    }

    public IEnumerator Join() {
        StartMatchMaker();
        matchMaker.ListMatches(0, 1, "", true, 0, 0, OnMatchList);
        yield return new WaitForSeconds(0.5f);
        foreach (MatchInfoSnapshot m in matches) {
            if (m.name == matchName) {
                _match = m;
            }
        }
        List<MatchInfoSnapshot.MatchInfoDirectConnectSnapshot> a = _match.directConnectInfos;
        matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, OnMatchJoined);
    }

    public void EndGame() {
        Application.Quit();
    }

}
