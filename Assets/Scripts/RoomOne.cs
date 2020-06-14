/// <ClassInfo>
/// __Bugs gefunden:__
///     _#001_(13.06.20)
///         Hier wird die Hand immer wieder auf "NULL" gesetzt. Somit kann die Rotation des Controllers beim InterfaceY niemals erkannt werden, 
///         da die Hand ja immer wieder herausgelöscht wird.
///     _#002_(13.06.20)
///         Bug liegt höchstwahrscheinlich in der SwitchCamera-Methode. Manchmal ist es möglich das Interface zu aktivieren (also Kamera zu wechseln)
///         obwohl das Interface gar nicht berührt wird. Tritt immer nur dann auf nachdem das Interface einmal benutzt wurde. Dann kann man es EINMAL
///         machen. Danach tritt der Bug nicht mehr auf. (Note for future-Rico: Hoffentlich verstehst du das morgen noch)
///     
/// __Bugs behoben:__
///     Bug#001 -> gefixt. Hand wird nun nicht immer wieder auf NULL gesetzt, sondern nur wenn weder "isInteracting", noch "isInteractingY" 
///                auf TRUE stehen.
///     Bug#002 -> fixed
///     
/// </ClassInfo>

using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RoomOne : MonoBehaviour {

    public bool Solved { get; set; }

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
            RotateX();
        } else if (isInteractingY) {
            Player.GetComponent<PlayerMovement>().enabled = false;
            RotateY();
        } else {
            Player.GetComponent<PlayerMovement>().enabled = true;
            interfacingHand = null;
        }
    }

    private void RotateX() {
        if (interatingHandPosition != null) {
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
    }

    private void RotateY() {
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
    }

    private void SwitchCameraX() { // Bug#002
        if (InterfaceX.GetComponent<InterfaceManager>().Activated && TopCamera.activeInHierarchy) {
            if (!interfacingHand.grabPinchAction.state) {
                isInteracting = false;
                InterfaceX.GetComponent<InterfaceManager>().Activated = false;
                StartCoroutine(FadeAndSwitch(TopCamera, Camera));
            }
        } else if (InterfaceX.GetComponent<InterfaceManager>().Activated && !TopCamera.activeInHierarchy && (LeftHand.grabPinchAction.GetState(LeftHand.handType) || RightHand.grabPinchAction.GetState(RightHand.handType))) {
            if (RightHand.grabPinchAction.GetState(RightHand.handType) && RightHand.hoveringInteractable.name == "InterfaceX") {
                interfacingHand = RightHand;
            } else if (LeftHand.grabPinchAction.GetState(LeftHand.handType) && LeftHand.hoveringInteractable.name == "InterfaceX") {
                interfacingHand = LeftHand;
            } else {
                return;
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
        } else if (InterfaceY.GetComponent<InterfaceManager>().Activated && !TopCamera.activeInHierarchy && (LeftHand.grabPinchAction.GetState(LeftHand.handType) || RightHand.grabPinchAction.GetState(RightHand.handType))) {
            if (RightHand.grabPinchAction.GetState(RightHand.handType) && RightHand.hoveringInteractable.name == "InterfaceY") {
                interfacingHand = RightHand;
            } else if (LeftHand.grabPinchAction.GetState(LeftHand.handType) && LeftHand.hoveringInteractable.name == "InterfaceY") {
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
