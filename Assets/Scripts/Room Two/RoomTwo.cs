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
            }
        }
    }

    private bool TargetCollision(GameObject cube) {
        return cube.GetComponent<CheckTargetCollision>().TargetCollision;
    }
}
