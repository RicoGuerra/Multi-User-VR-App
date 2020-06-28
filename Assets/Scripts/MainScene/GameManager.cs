using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] rooms;
    public GameObject[] players;

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

    public void EndGame() {
        //ending the game
        Application.Quit();
    }
}
