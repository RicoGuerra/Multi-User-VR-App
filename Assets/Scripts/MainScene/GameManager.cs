using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] rooms;
    public GameObject[] players;
    public GameObject[] PassageWays;

    private List<GameObject> playersInCorridor = new List<GameObject>();

    private void Update() {
        if (players.Length < 2) {
            players = GameObject.FindGameObjectsWithTag("Player");
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
    }

    public void PlayerLeavingCorridor(GameObject corridor) {
        GameObject playerObject = corridor.GetComponentInChildren<CheckTargetCollision>().TargetObjectInfo;
        playersInCorridor.Remove(playerObject);
        if (playerObject.transform.position.z < corridor.transform.position.z && playersInCorridor.Count == 0)
            DeactivateCorridor(corridor);
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
            } else {
                Debug.Log("Colliding object does not match any existing corridor in the scene or there are not exactly 2 different objects in the list.");
            }
        }
    }

    public static void EndGame() {
        //ending the game
        Application.Quit();
    }
}
