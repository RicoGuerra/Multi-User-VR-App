using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] rooms;
    public GameObject[] players;
    public GameObject[] PassageWays;
    public BorderManager BorderManager;

    private GameObject[] _passivePlayers;
    private List<GameObject> playersInCorridor = new List<GameObject>();
    /// <summary>
    /// Type = 0 >> NONVR-Player
    /// Type = 1 >> VR-Player
    /// </summary>
    private int _localPlayerType;
    private bool _playerTypeSet;
    private UnityEvent _borderManagement;

    private void Start() {
        _borderManagement = new UnityEvent();
        _borderManagement.AddListener(BorderManager.DeactivateAllBorders);
    }

    private void Update() {
        if (players.Length < 2) {
            players = GameObject.FindGameObjectsWithTag("Player");
        }
    }

    public void SetLocalPlayerType(PlayerManager player) {
        if (XRDevice.isPresent) {
            _localPlayerType = 1;
        } else {
            _localPlayerType = 0;
            _borderManagement.Invoke();
        }
    }

    public void WakeUpRoom(GameObject room) {
        //instatiate or activate all behaviours and objects
        room.SetActive(true);
    }

    public void DeactivateRoom(GameObject room) {
        //destroy or deactivate all behaviours and objects
        room.SetActive(false);
    }

    public void ActivateCorridor(int corridorIndex) {
        PassageWays[corridorIndex].SetActive(true);
        BorderManager.CorridorToggle(1, corridorIndex); // opt 1
    }

    public void PlayerLeavingCorridor(GameObject corridor) {
        GameObject playerObject = corridor.GetComponentInChildren<CheckTargetCollision>().TargetObjectInfo;
        playersInCorridor.Remove(playerObject);
        if (playerObject.transform.position.z < corridor.transform.position.z && playersInCorridor.Count == 0) {
            // opt 0
            BorderManager.CorridorToggle(0, GetCorridorIndex(corridor));
            DeactivateCorridor(corridor);
        }
    }

    public void DeactivateCorridor(GameObject corridor) {
        corridor.SetActive(false);
    }

    public void RoomTransition(GameObject corridor) {
        GameObject playerObject = corridor.GetComponentInChildren<CheckTargetCollision>().TargetObjectInfo;
        if (!playersInCorridor.Contains(playerObject)) {
            playersInCorridor.Add(playerObject);
        } else {
            return;
        }
        for (int i = 0; i < PassageWays.Length; i++) {
            if (corridor == PassageWays[i] && playersInCorridor.Count == 2) {
                WakeUpRoom(rooms[i + 1]);
                DeactivateRoom(rooms[i]);
                // opt 2
                BorderManager.CorridorToggle(2, i);
            } else {
                Debug.Log("Colliding object does not match any existing corridor in the scene or there are not exactly 2 different objects in the list.");
            }
        }
    }

    public int GetCorridorIndex(GameObject corridor) {
        for (int i = 0; i < PassageWays.Length; i++) {
            if (PassageWays[i] == corridor) return i;
        }
        Debug.LogError("Corridor existier nicht");
        return 2;
    }


    public static void EndGame() {
        //ending the game
        Application.Quit();
    }
}
