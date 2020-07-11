using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTwo : MonoBehaviour {

    public List<GameObject> RiddleRight;

    void Update() {
        if (RiddleRight.TrueForAll(TargetCollision)) {
            Debug.Log("___CUBES ARE IN THE CORECT ORDER___");
        }
    }

    private bool TargetCollision(GameObject cube) {
        return cube.GetComponent<CheckTargetCollision>().TargetCollision;
    }
}
