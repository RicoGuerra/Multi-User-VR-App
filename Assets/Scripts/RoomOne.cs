using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RoomOne : MonoBehaviour {

    public GameObject InterfaceX;
    public GameObject InterfaceY;
    public GameObject TopCamera; //wird noch zu "CameraPosition o.Ä"
    public GameObject Camera;
    public float FadeTime;

    private bool isSwitching;

    //fade and camera transition testing
    private void Update() {
        if (Input.GetKeyDown(KeyCode.K) && TopCamera.activeInHierarchy) {
            StartCoroutine(FadeAndSwitch(TopCamera, Camera));
        } else if (Input.GetKeyDown(KeyCode.K) && Camera.activeInHierarchy) {
            StartCoroutine(FadeAndSwitch(Camera, TopCamera));
        }
        //nur als Test. Eigentlich sobald Interface aktiviert wird
        //und Knopf auf Controller gedrückt wird
    }

    private IEnumerator FadeAndSwitch(GameObject from, GameObject to) {
        SteamVR_Fade.Start(Color.black, FadeTime, true);

        yield return new WaitForSeconds(FadeTime);
        from.SetActive(false);
        to.SetActive(true);

        SteamVR_Fade.Start(Color.clear, FadeTime, true);
    }
}
