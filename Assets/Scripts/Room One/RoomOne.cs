using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RoomOne : Room {

    public GameObject InterfaceX;
    public GameObject InterfaceY;
    public GameObject Kugellabyrinth;
    public GameObject Kugel;
    public GameObject TopCameraY;
    public GameObject TopCamera;
    public GameObject Camera;

    [SerializeField] private GameObject _handleX;
    [SerializeField] private GameObject _handleY;
    private float _fadeTime;
    private Vector3 kugelOrigin;
    private bool _XLocked;
    private bool _YLocked;
    private bool _usingHandleX;

    public UnityEvent LockInterfaceX;
    public UnityEvent LockInterfaceY;

    private void Start() {
        RoomNumber = 1;
        CorridorToActivate = 0;
        kugelOrigin = Kugel.transform.position;
        _fadeTime = 0.5f;
    }

    private void Update() {
        Rotate();
        if (Kugel.transform.position.y < -15) {
            BringBallBack(Kugel, kugelOrigin);
        }
        if (_handleX.transform.localPosition.x != 0 && InterfaceX.GetComponent<InterfaceManager>().PlayerOnInterface == null) {
            LockInterfaceX.Invoke();
        } else if (_handleY.transform.localPosition.z != 0 && InterfaceY.GetComponent<InterfaceManager>().PlayerOnInterface == null) {
            LockInterfaceY.Invoke();
        }
    }

    private void Rotate() {
        Kugellabyrinth.transform.rotation = Quaternion.Euler(_handleY.transform.localPosition.z * 100, 0, -_handleX.transform.localPosition.x * 100);
    }

    public void SwitchCameraX() { 
        if (!InterfaceX.GetComponent<InterfaceManager>().Activated && InterfaceX.GetComponent<InterfaceManager>().PlayerOnInterface != null) {
            StartCoroutine(FadeAndSwitch(TopCamera, InterfaceX.GetComponent<InterfaceManager>().PlayerOnInterface.GetCamera()));
        } else if (InterfaceX.GetComponent<InterfaceManager>().Activated) {
            StartCoroutine(FadeAndSwitch(InterfaceX.GetComponent<InterfaceManager>().PlayerOnInterface.GetCamera(), TopCamera));
        }
    }

    public void SwitchCameraY() {
        if (!InterfaceY.GetComponent<InterfaceManager>().Activated && InterfaceY.GetComponent<InterfaceManager>().PlayerOnInterface != null) {
            StartCoroutine(FadeAndSwitch(TopCameraY, InterfaceY.GetComponent<InterfaceManager>().PlayerOnInterface.GetCamera()));
        } else if (InterfaceY.GetComponent<InterfaceManager>().Activated) {
            StartCoroutine(FadeAndSwitch(InterfaceY.GetComponent<InterfaceManager>().PlayerOnInterface.GetCamera(), TopCameraY));
        }
    }

    private IEnumerator FadeAndSwitch(GameObject from, GameObject to) {
        SteamVR_Fade.Start(Color.black, _fadeTime, true);

        yield return new WaitForSeconds(_fadeTime);
        from.SetActive(false);
        to.SetActive(true);

        SteamVR_Fade.Start(Color.clear, _fadeTime, true);
    }

    private GameObject GetRootOfHand(Hand hand) {
        return hand.transform.root.gameObject;
    }
}
