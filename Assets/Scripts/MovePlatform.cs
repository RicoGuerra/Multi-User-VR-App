using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour {

    private bool PlayerOnPlatform;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            other.transform.parent = transform;
            PlayerOnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            other.transform.parent = null;
            PlayerOnPlatform = false;
        }
    }




}
