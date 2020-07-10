using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BowlingPlatform : MonoBehaviour {

    public bool BallIsColliding { get; set; }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Big Ball") {
            BallIsColliding = true;
        } else {
            BallIsColliding = false;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.name == "Big Ball")
            BallIsColliding = false;
    }
}
