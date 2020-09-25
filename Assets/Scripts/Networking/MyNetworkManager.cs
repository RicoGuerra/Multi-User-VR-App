using Assets.Scripts.Menu;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class MyNetworkManager : NetworkManager {

    private MatchInfoSnapshot _match;
    [SerializeField] private UnityEvent _matchExists;
    [SerializeField] private UnityEvent _matchDoesntExist;

    private void Start() {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        if (!XRDevice.isPresent) {
            //playerPrefab = spawnPrefabs.First();
        }
    }

    private void ChangedActiveScene(Scene current, Scene next) {
        GameObject[] canvas = GameObject.FindGameObjectsWithTag("Canvas");
        if (next.buildIndex == 0) {
            foreach (GameObject c in canvas) {
                c.SetActive(false);
            }
            GameObject.Find("Menu").GetComponent<MenuManager>().EndCountDown.SetActive(true);
        }
        Debug.Log("Scene: " + next.name);
    }

    public void StartMatch() {
        StartCoroutine(StartingMatch());
        //StartMatchMaker();
        //matchMaker.CreateMatch(matchName, matchSize, true, "", "", "", 0, 0, OnMatchCreate);
    }

    public IEnumerator StartingMatch() {
        StartMatchMaker();
        matchMaker.ListMatches(0, 1, "", true, 0, 0, OnMatchList);
        yield return new WaitForSeconds(0.5f);
        if (matches.Exists(x => x.name == matchName)) {
            Debug.LogError("Match already exitst! Please press SPIEL BEITRETEN to join the match! ");
            _matchExists.Invoke();
            StopMatchMaker();
        } else {
            matchMaker.CreateMatch(matchName, matchSize, true, "", "", "", 0, 0, OnMatchCreate);
        }
    }

    public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList) {
        matches = matchList;
        base.OnMatchList(success, extendedInfo, matchList);
    }

    public void JoinMatch() {
        StartCoroutine(JoiningMatch());
    }

    public IEnumerator JoiningMatch() {
        StartMatchMaker();
        matchMaker.ListMatches(0, 1, "", true, 0, 0, OnMatchList);
        yield return new WaitForSeconds(0.5f);
        foreach (MatchInfoSnapshot m in matches) {
            if (m.name == matchName) {
                _match = m;
            }
        }
        if (_match == null) {
            _matchDoesntExist.Invoke();
            StopMatchMaker();
        }
        List<MatchInfoSnapshot.MatchInfoDirectConnectSnapshot> a = _match.directConnectInfos;
        matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, OnMatchJoined);
    }

    public void EndGame() {
        Application.Quit();
    }

}
