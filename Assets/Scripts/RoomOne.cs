using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RoomOne : MonoBehaviour {

    public GameObject InterfaceX;
    public GameObject InterfaceY;
    public GameObject TopCamera; //wird noch zu "CameraPosition o.Ä"
    public GameObject Camera;
    public float FadeTime;
    public Hand RightHand;
    public Hand LeftHand;

    private bool isSwitching;

    //fade and camera transition testing
    private void Update() {
        SwitchCamera();
    }

    private void SwitchCamera() {
        if (InterfaceX.GetComponent<InterfaceManager>().Activated && TopCamera.activeInHierarchy && (LeftHand.grabPinchAction.state || RightHand.grabPinchAction.state)) {
            StartCoroutine(FadeAndSwitch(TopCamera, Camera));
        } else if (InterfaceX.GetComponent<InterfaceManager>().Activated && !TopCamera.activeInHierarchy && (LeftHand.grabPinchAction.state || RightHand.grabPinchAction.state)) {
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
