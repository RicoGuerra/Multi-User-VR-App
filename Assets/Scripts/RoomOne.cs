using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RoomOne : MonoBehaviour {

    public GameObject InterfaceX;
    public GameObject InterfaceY;
    public GameObject Kugellabyrinth;
    public GameObject TopCamera; //wird noch zu "CameraPosition o.Ä"
    public GameObject Camera;
    public float FadeTime;
    public Hand RightHand;
    public Hand LeftHand;

    private bool isInteracting;
    private bool handRight;
    private Vector3 interatingHandPosition;
    private Quaternion interatingHandRotation;
    private Hand interfacingHand;

    private void Update() {
        SwitchCamera();
        if (isInteracting) {
            interatingHandPosition = interfacingHand.gameObject.transform.position;
            interatingHandRotation = interfacingHand.gameObject.transform.rotation;
            Kugellabyrinth.transform.Rotate(Vector3.forward, interatingHandPosition.x/5, Space.Self);
        }
    }

    private void SwitchCamera() {
        if (InterfaceX.GetComponent<InterfaceManager>().Activated && TopCamera.activeInHierarchy) {
            if (!interfacingHand.grabPinchAction.state) {
                isInteracting = false;
                StartCoroutine(FadeAndSwitch(TopCamera, Camera));
            }
        } else if (InterfaceX.GetComponent<InterfaceManager>().Activated && !TopCamera.activeInHierarchy && (LeftHand.grabPinchAction.state || RightHand.grabPinchAction.state)) {
            if (RightHand.grabPinchAction.state) {
                interfacingHand = RightHand;
            } else if (LeftHand.grabPinchAction.state) {
                interfacingHand = LeftHand;
            }
            isInteracting = true;
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
