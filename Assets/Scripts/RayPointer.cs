using UnityEngine;
using UnityEngine.EventSystems;

public class RayPointer : MonoBehaviour {

    public float DefaultLength = 5.0f;
    public GameObject Dot;
    public VRInputModule InputModule;

    private LineRenderer lineRenderer;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update() {
        UpdateLine();
    }

    private void UpdateLine() {
        PointerEventData data = InputModule.Data;
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? DefaultLength : data.pointerCurrentRaycast.distance;

        RaycastHit hit = CreateRayCast(targetLength);
        Vector3 endPosition = transform.position + (transform.forward * targetLength);
        if (hit.collider != null)
            endPosition = hit.point;
        Dot.transform.position = endPosition;
        lineRenderer.SetPosition(0,transform.position);
        lineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRayCast(float length) {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit);
        return hit;
    }
}
