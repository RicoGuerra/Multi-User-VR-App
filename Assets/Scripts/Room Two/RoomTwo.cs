using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTwo : Room {

    public List<GameObject> RiddleRight;

    private bool rightSolved;
    private bool leftSolved;
    private AudioSource successSound;

    private void Start() {
        successSound = GetComponent<AudioSource>();
    }

    void Update() {
        if (RiddleRight.TrueForAll(TargetCollision)) {
            if (!rightSolved) {
                successSound.Play();
                rightSolved = true;
                GameObject.Find("ExitRight").SetActive(false);
            }
        }
        Entrance("EntranceRight");
    }

    public void WriteLog() {
        Debug.Log("___BUTTUN-IS-DOWN!!____");
    }

    private static void Entrance(string enter) {
        GameObject entrance = GameObject.Find(enter);
        if (entrance.GetComponent<CheckTargetCollision>().TargetTriggerExit) {
            entrance.GetComponent<MeshRenderer>().enabled = true;
            entrance.GetComponent<Collider>().isTrigger = false;
        }
    }

    private bool TargetCollision(GameObject cube) {
        return cube.GetComponent<CheckTargetCollision>().TargetCollision;
    }
}
