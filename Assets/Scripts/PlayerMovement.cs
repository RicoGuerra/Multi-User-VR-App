using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerMovement : MonoBehaviour {

    public SteamVR_Action_Vector2 Move;
    public float Speed;
    public GameObject Head;

    void Update() {
        MovePlayer();
    }

    private void MovePlayer() {
        Quaternion orientation = Orientation();
        Vector3 movement = Vector3.zero;
        movement += orientation * (Speed * Vector3.forward);
        if (Move.axis.magnitude != 0) {
            transform.localPosition += movement * Time.deltaTime;
        }
    }

    private Quaternion Orientation() {
        float rotation = Mathf.Atan2(Move.axis.x, Move.axis.y);
        rotation *= Mathf.Rad2Deg;

        Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y + rotation, 0);
        return Quaternion.Euler(orientationEuler);
    }
}
