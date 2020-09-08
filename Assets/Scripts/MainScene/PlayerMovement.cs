using UnityEngine;
using Valve.VR;

public class PlayerMovement : MonoBehaviour {

    public SteamVR_Action_Vector2 Move;
    public float Speed;
    public bool Interface;

    void Update() {
        if(!Interface)
            MovePlayer();
    }

    private void MovePlayer() {
        Quaternion orientation = Orientation();
        Vector3 movement = Vector3.zero;
        movement += orientation * (Speed * Vector3.forward);
        if (Move.axis.magnitude != 0) {
            //transform.Translate(movement * Time.deltaTime);
            GetComponent<CharacterController>().Move(movement * Time.deltaTime);
            //GetComponent<Rigidbody>().MovePosition(movement * Time.deltaTime);
        }
    }

    private Quaternion Orientation() {
        float rotation = Mathf.Atan2(Move.axis.x, Move.axis.y);
        rotation *= Mathf.Rad2Deg;
        Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y + rotation, 0);
        return Quaternion.Euler(orientationEuler);
    }
}
