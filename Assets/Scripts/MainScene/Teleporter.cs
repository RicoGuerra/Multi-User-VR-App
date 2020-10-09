using System.Collections;
using UnityEngine;
using Valve.VR;

public class Teleporter : MonoBehaviour {
    public GameObject Pointer;
    public SteamVR_Action_Boolean TeleportAction;
    public SteamVR_Action_Boolean TriggerDown;

    private SteamVR_Behaviour_Pose pose = null;
    private bool hasPosition = false;
    private bool isTeleporting = false;
    private float fadeTime = 0.5f;
    private float hitPointY;
    private RaycastHit _hit;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Material _teleportOK;
    [SerializeField] private Material _teleportNotOK;

    void Awake() {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    void Update() {
        hasPosition = UpdatePointer();
        Pointer.SetActive(hasPosition);
        _lineRenderer.enabled = hasPosition;
        SetPointerMaterial();

        if (TeleportAction.GetStateUp(pose.inputSource) && _hit.collider.tag == "Floor" && _hit.distance <= 10f) {
            TryTeleport();
        }
    }

    private void SetPointerMaterial() {
        if (_hit.collider.tag == "Floor" && _hit.distance <= 10f) {
            Pointer.GetComponent<Renderer>().material = _lineRenderer.material = _teleportOK;
        } else {
            Pointer.GetComponent<Renderer>().material = _lineRenderer.material = _teleportNotOK;
        }
    }

    void TryTeleport() {
        if (!hasPosition || isTeleporting) {
            return;
        }
        Transform camera = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        Vector3 groundPosition = new Vector3(headPosition.x, camera.position.y, headPosition.z);
        Vector3 translationVector = Pointer.transform.position - groundPosition;

        StartCoroutine(MoveRig(camera, translationVector));
    }

    IEnumerator MoveRig(Transform camera, Vector3 translation) {
        isTeleporting = true;

        SteamVR_Fade.Start(Color.black, fadeTime, true);

        yield return new WaitForSeconds(fadeTime);
        camera.transform.root.position += new Vector3(translation.x, 0, translation.z);

        SteamVR_Fade.Start(Color.clear, fadeTime, true);

        isTeleporting = false;
    }

    bool UpdatePointer() {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.SphereCast(ray, 0.25f, out _hit) && TriggerDown.GetState(pose.inputSource)) {
            Pointer.transform.position = _hit.point;
            _lineRenderer.SetPosition(0, ray.origin);
            _lineRenderer.SetPosition(1, _hit.point);
            return true;
        }
        return false;
    }
}
