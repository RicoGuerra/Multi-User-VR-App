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

    void Awake() {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    void Update() {
        hasPosition = UpdatePointer();
        Pointer.SetActive(hasPosition);

        if (TeleportAction.GetStateUp(pose.inputSource) && hitPointY < 0.55f) {
            TryTeleport();
        }
    }

    void TryTeleport() {
        if(!hasPosition || isTeleporting) {
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
        camera.position += translation;

        SteamVR_Fade.Start(Color.clear, fadeTime, true);

        isTeleporting = false;
    }

    bool UpdatePointer() {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && TriggerDown.GetState(pose.inputSource)) {
            hitPointY = hit.point.y;
            Pointer.transform.position = hit.point;
            return true;
        }
        return false;
    }
}
