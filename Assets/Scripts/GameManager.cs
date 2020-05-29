using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] rooms;
    private GameObject[] players;

    public void WakeUpRoom(GameObject room) {
        //instatiate or activate all behaviours and objects
    }

    public void EndGame() {
        //ending the game
    }
}
