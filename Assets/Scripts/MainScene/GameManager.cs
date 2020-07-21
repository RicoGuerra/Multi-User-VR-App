using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] rooms;
    public GameObject[] players;
    public GameObject[] PassageWays;

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

    public void DeactivateCorridor(int corridorIndex) {
        PassageWays[corridorIndex].SetActive(false);
    }

    public void RoomTransition(GameObject corridor) { 
        // funktioniert nicht, da es dem Spieler nicht möglich ist Funktionen des GameManagers aufzurufen.
        // Entweder muss der GameManager bzw das Skript ein Singleton sein, welches global verfügbar ist, oder die Kollisionserkennung muss von den Korridoren ausgehen
        if (players[0].GetComponent<CheckTargetCollision>().TargetCollisionEnter && players[1].GetComponent<CheckTargetCollision>().TargetCollisionEnter) {
            if (corridor == PassageWays[0]) {
                WakeUpRoom(rooms[1]);
                DeactivateRoom(rooms[0]);
            } else if(corridor == PassageWays[1]) {
                WakeUpRoom(rooms[2]);
                DeactivateRoom(rooms[1]);
            } else {
                Debug.LogError("Colliding object does not match any existing corridor in the scene!");
            }
        }
    }

    public void EndGame() {
        //ending the game
        Application.Quit();
    }
}
