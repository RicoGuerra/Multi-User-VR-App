/// <ClassInfo>
/// __Allg. Infos:__
///     _Info1_(26.06.20)
///         TopCamera & TopCameraY koennen in der Szene lokal aktiviert werden, ohne, dass es interferenzen mit der Kamera des anderen Spielers gibt. 
///         Dies ist jedoch noch ausgiebiger zu testen.
/// 
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

using Assets.Scripts.MainScene;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
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

    public void SwitchCameraX() { // Bug#002
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
