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
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RoomOne : Room {

    public GameObject Player;
    public GameObject InterfaceX;
    public GameObject InterfaceY;
    public GameObject Kugellabyrinth;
    public GameObject Kugel;
    public GameObject TopCameraY;
    public GameObject TopCamera;
    public GameObject Camera;
    public float FadeTime;
    public Hand RightHand;
    public Hand LeftHand;

    [SerializeField] private GameObject _handleX;
    [SerializeField] private GameObject _handleY;

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
    private int transitionWaitX;
    private Vector3 kugelOrigin;

    private void Start() {
        CorridorToActivate = 0;
        kugelOrigin = Kugel.transform.position;
    }

    private void Update() {

        /* OnHandAttached() beim Handle-Objekt setzt das Activated-Attribut der Interfaces auf TRUE;
         * Dadurch werden beide "IsInteracting"-Variablen nicht mehr gebraucht;
         * Die ROTATE-Methoden können deutlich vereinfacht werden, siehe oben. Und die SWITCHCAMERA-Methoden können ebenfalls vereinfacht werden, 
         * da alle Abfragen, die die Hände betreffen komplett wegfallen
         */
        //RotateX();
        //RotateY();
        Rotate();
        if (Kugel.transform.position.y < -15)
            BringBallBack(Kugel, kugelOrigin);
    }

    private void RotateX() {
        if (InterfaceX.GetComponent<InterfaceManager>().Activated && TopCamera.activeInHierarchy)
            Kugellabyrinth.transform.rotation = Quaternion.Euler(0, 0, -_handleX.transform.localPosition.x * 100);
    }

    private void RotateY() {
        if (InterfaceY.GetComponent<InterfaceManager>().Activated && TopCameraY.activeInHierarchy)
            Kugellabyrinth.transform.rotation = Quaternion.Euler(_handleY.transform.localPosition.z * 100, 0, -_handleX.transform.localPosition.x * 100);
    }

    private void Rotate() {
        if ((InterfaceY.GetComponent<InterfaceManager>().Activated || InterfaceX.GetComponent<InterfaceManager>().Activated) && (TopCameraY.activeInHierarchy || TopCamera.activeInHierarchy))
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
        SteamVR_Fade.Start(Color.black, FadeTime, true);

        yield return new WaitForSeconds(FadeTime);
        from.SetActive(false);
        to.SetActive(true);

        SteamVR_Fade.Start(Color.clear, FadeTime, true);
    }
}
