using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTwo : Room {

    public List<GameObject> RiddleRight;

    private int[] riddleLeftOrder;
    private int buttonCounter;
    private bool rightSolved;
    private bool leftSolved;
    private AudioSource successSound;

    private void Start() {
        successSound = GetComponent<AudioSource>();
        riddleLeftOrder = new int[4];
        buttonCounter = 0;
        CorridorToActivate = 1;
    }

    void Update() {
        if(rightSolved && leftSolved) {
            Solved = true;
        }
        if (RiddleRight.TrueForAll(TargetCollision)) {
            if (!rightSolved) {
                successSound.Play();
                rightSolved = true;
                GameObject.Find("ExitRight").SetActive(false);
            }
        }
    }

    public void CheckRiddleLeft(int buttonIndex) {
        riddleLeftOrder[buttonCounter] = buttonIndex;
        buttonCounter++;
        if (riddleLeftOrder[0] == 2 && riddleLeftOrder[1] == 1 && riddleLeftOrder[2] == 0 && riddleLeftOrder[3] == 4 && !leftSolved) {
            successSound.Play();
            leftSolved = true;
            GameObject.Find("ExitLeft").SetActive(false);
        } else if (buttonCounter == 4) {
            buttonCounter = 0;
            for (int i = 0; i < riddleLeftOrder.Length; i++)
                riddleLeftOrder[i] = 0;
        }
    }

    public void Entrance(string enter) {
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
