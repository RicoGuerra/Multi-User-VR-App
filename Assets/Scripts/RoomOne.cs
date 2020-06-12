using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RoomOne : MonoBehaviour {

    public GameObject Player;
    public GameObject InterfaceX;
    public GameObject InterfaceY;
    public GameObject Kugellabyrinth;
    public GameObject TopCamera; //wird noch zu "CameraPosition o.Ä"
    public GameObject Camera;
    public float FadeTime;
    public Hand RightHand;
    public Hand LeftHand;

    private bool isInteracting;
    private bool isInteractingY;
    private bool handRight;
    private Vector3 interatingHandPosition;
    private Quaternion interatingHandRotation;
    private Hand interfacingHand;
    private Vector3 currHandPos;
    private Vector3 prevHandPos;
    private float rotationX;
    private float rotationY;

    private void Update() {
        SwitchCameraX();
        SwitchCameraY();
        if (isInteracting) {
            Player.GetComponent<PlayerMovement>().enabled = false;
            if(interatingHandPosition != null) {
                prevHandPos = interatingHandPosition;
            }
            interatingHandPosition = interfacingHand.gameObject.transform.position;
            if (prevHandPos.x > interatingHandPosition.x) {
                rotationX = prevHandPos.x - interatingHandPosition.x;
                Kugellabyrinth.transform.Rotate(0, 0, rotationX * 75);
            } else if (prevHandPos.x < interatingHandPosition.x) {
                rotationX = interatingHandPosition.x - prevHandPos.x;
                Kugellabyrinth.transform.Rotate(0, 0, -rotationX * 75);
            }
        } else {
            Player.GetComponent<PlayerMovement>().enabled = true;
            interfacingHand = null;
        }

        if (isInteractingY) {
            Player.GetComponent<PlayerMovement>().enabled = false;
            if (interatingHandPosition != null) {
                prevHandPos = interatingHandPosition;
            }
            interatingHandPosition = interfacingHand.gameObject.transform.position;
            if (prevHandPos.z > interatingHandPosition.z) {
                rotationY = prevHandPos.z - interatingHandPosition.z;
                Kugellabyrinth.transform.Rotate(rotationY * 75, 0, 0);
            } else if (prevHandPos.z < interatingHandPosition.z) {
                rotationY = interatingHandPosition.z - prevHandPos.z;
                Kugellabyrinth.transform.Rotate(-rotationY * 75, 0, 0);
            }
        } else {
            Player.GetComponent<PlayerMovement>().enabled = true;
            interfacingHand = null;
        }
    }

    private void SwitchCameraX() {
        if (InterfaceX.GetComponent<InterfaceManager>().Activated && TopCamera.activeInHierarchy) {
            if (!interfacingHand.grabPinchAction.state) {
                isInteracting = false;
                InterfaceX.GetComponent<InterfaceManager>().Activated = false;
                StartCoroutine(FadeAndSwitch(TopCamera, Camera));
            }
        } else if (InterfaceX.GetComponent<InterfaceManager>().Activated && !TopCamera.activeInHierarchy && (LeftHand.grabPinchAction.state || RightHand.grabPinchAction.state)) {
            Debug.Log("INTERFACE");
            if (RightHand.grabPinchAction.state) {
                interfacingHand = RightHand;
            } else if (LeftHand.grabPinchAction.state) {
                interfacingHand = LeftHand;
            }
            isInteracting = true;
            StartCoroutine(FadeAndSwitch(Camera, TopCamera));
        }
    }

    private void SwitchCameraY() {
        if (InterfaceY.GetComponent<InterfaceManager>().Activated && TopCamera.activeInHierarchy) {
            if (!interfacingHand.grabPinchAction.state) {
                isInteractingY = false;
                InterfaceY.GetComponent<InterfaceManager>().Activated = false;
                StartCoroutine(FadeAndSwitch(TopCamera, Camera));
            }
        } else if (InterfaceY.GetComponent<InterfaceManager>().Activated && !TopCamera.activeInHierarchy && (LeftHand.grabPinchAction.state || RightHand.grabPinchAction.state)) {
            if (RightHand.grabPinchAction.state) {
                interfacingHand = RightHand;
            } else if (LeftHand.grabPinchAction.state) {
                interfacingHand = LeftHand;
            }
            isInteractingY = true;
            StartCoroutine(FadeAndSwitch(Camera, TopCamera));
        }
    }

    private IEnumerator FadeAndSwitch(GameObject from, GameObject to) {
        SteamVR_Fade.Start(Color.black, FadeTime, true);

        yield return new WaitForSeconds(FadeTime);
        from.SetActive(false);
        to.SetActive(true);

        SteamVR_Fade.Start(Color.clear, FadeTime, true);
    }
}
