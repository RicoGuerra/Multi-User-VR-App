using System.Collections;
using UnityEngine;

public class MovePlatform : MonoBehaviour {

    private bool playerOnPlatform;
    private bool floorY;
    private bool floorX;
    private Vector3 moveVector;
    private Vector3 posFloorX;
    private Vector3 posFloorY;
    private int frameCount;

    private void Start() {
        posFloorX = transform.position;
        posFloorY = new Vector3(15, 0, 7.5f);
        frameCount = 0;
        moveVector = new Vector3(0, 0, 0.1f);
    }

    void Update() {
        if (playerOnPlatform && !floorY) {
            frameCount++;
            if (frameCount > 180) {
                Move();
            }
        } else if (floorY) {
            Move();
        } else {
            frameCount = 0;
        }
    }

    private void Move() {
        if (transform.position.z >= posFloorX.z) {
            floorY = false;
            floorX = true;
        }
        if (transform.position.z <= posFloorY.z) {
            floorX = false;
            floorY = true;
        }
        if (floorX) {
            transform.position -= moveVector;
        }
        if (floorY && !playerOnPlatform) {
            frameCount--;
            if (frameCount < -240)
                transform.position += moveVector;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.transform.parent = transform;
            playerOnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            other.transform.parent = null;
            playerOnPlatform = false;
        }
    }



}
