using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerMovement : MonoBehaviour {

    public SteamVR_Action_Vector2 Move;
    public float Speed;

    void Update() {
        //if (Move.axis.y > 0.05)
        //transform.localPosition += (transform.forward + new Vector3(Move.axis.x, 0, Move.axis.y)) * Time.deltaTime * Speed;
        //MoveWithHeadDirection();
        //MoveWithoutHeadDirection();
    }

    private void MoveWithHeadDirection() {
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(Move.axis.x, Move.axis.y, 0));
        transform.position += Speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
    }

    private void MoveWithoutHeadDirection() {
        transform.position += Speed * Time.deltaTime * new Vector3(Move.axis.x, 0, Move.axis.y);
    }
}
