using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicMovement : MonoBehaviour {

    public GameObject RollerBall;

    void Update() {
        Mimic(transform, RollerBall.transform);
    }

    public void Mimic(Transform localBall, Transform globalBall) {
        localBall.position = globalBall.position;
        localBall.rotation = globalBall.rotation;
    }
}
