using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] _rooms;
    [SerializeField]
    private GameObject[] _players;
    [SerializeField]
    private GameObject[] _passageWays;
    [SerializeField]
    private BorderManager _borderManager;
    [SerializeField]
    private GameObject _wonObject;

    private List<GameObject> _playersInCorridor = new List<GameObject>();
    /// <summary>
    /// Type = 0 >> NONVR-Player
    /// Type = 1 >> VR-Player
    /// </summary>
    private int _localPlayerType;
    private UnityEvent _borderManagement;

    private void Start() {
        _borderManagement = new UnityEvent();
        _borderManagement.AddListener(_borderManager.DeactivateAllBorders);
    }

    private void Update() {
        if (_players.Length < 2) {
            _players = GameObject.FindGameObjectsWithTag("Player");
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
        _passageWays[corridorIndex].SetActive(true);
        _borderManager.CorridorToggle(1, corridorIndex); // opt 1
    }

    public void PlayerLeavingCorridor(GameObject corridor) {
        GameObject playerObject = corridor.GetComponentInChildren<CheckTargetCollision>().TargetObjectInfo;
        _playersInCorridor.Remove(playerObject);
        if (playerObject.transform.position.z < corridor.transform.position.z && _playersInCorridor.Count == 0) {
            // opt 0
            _borderManager.CorridorToggle(0, GetCorridorIndex(corridor));
            DeactivateCorridor(corridor);
        }
    }

    public void DeactivateCorridor(GameObject corridor) {
        corridor.SetActive(false);
    }

    public void RoomTransition(GameObject corridor) {
        GameObject playerObject = corridor.GetComponentInChildren<CheckTargetCollision>().TargetObjectInfo;
        if (!_playersInCorridor.Contains(playerObject)) {
            _playersInCorridor.Add(playerObject);
        } else {
            return;
        }
        for (int i = 0; i < _passageWays.Length; i++) {
            if (corridor == _passageWays[i] && _playersInCorridor.Count == 2) {
                DeactivateRoom(_rooms[i]);
                // opt 2
                _borderManager.CorridorToggle(2, i);
            } else {
                Debug.Log("Colliding object does not match any existing corridor in the scene or there are not exactly 2 different objects in the list.");
            }
        }
    }

    public int GetCorridorIndex(GameObject corridor) {
        for (int i = 0; i < _passageWays.Length; i++) {
            if (_passageWays[i] == corridor) return i;
        }
        Debug.LogError("Corridor existier nicht");
        return 2;
    }

    public void GameWon() {
        _wonObject.SetActive(true);
    }

    public static void EndGame() {
        //ending the game
        Application.Quit();
    }
}
