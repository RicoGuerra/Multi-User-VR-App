using UnityEngine;

public class MovePlatform : MonoBehaviour {

    private bool PlayerOnPlatform;
    private bool floorY;
    private bool floorX;

    void Update() {
        if (!floorY) {
            transform.position -= new Vector3(0, 0, 0.075f);
        } else {
            transform.position += new Vector3(0, 0, 0.075f);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.transform.parent = transform;
            PlayerOnPlatform = true;
        }
        if (other.name == "FloorY") {
            floorY = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            other.transform.parent = null;
            PlayerOnPlatform = false;
        }
    }




}
