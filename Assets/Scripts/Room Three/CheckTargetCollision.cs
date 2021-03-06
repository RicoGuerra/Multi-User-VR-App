using UnityEngine;
using UnityEngine.Events;

public class CheckTargetCollision : MonoBehaviour {

    public bool TargetCollision { get; private set; }
    public bool TargetCollisionEnter { get; private set; }
    public bool TargetCollisionExit { get; private set; }
    public bool TargetTrigger { get; private set; }
    public bool TargetTriggerEnter { get; private set; }
    public bool TargetTriggerExit { get; private set; }
    [Tooltip("If there is not target needed, leave this empty")]
    public string TargetObject;
    public int CollisionCountTheshold;
    public int CollisionCount { get; private set; }

    public UnityEvent OnTargetCollisionEnter;
    public UnityEvent OnTargetTriggerEnter;
    public UnityEvent OnTargetCollisionExit;
    public UnityEvent OnTargetTriggerExit;
    public UnityEvent OnCollisionCount;
    public GameObject TargetObjectInfo { get; private set; }

    private void Update() {
        if (CollisionCount > CollisionCountTheshold && CollisionCountTheshold != 0)
            OnCollisionCount.Invoke();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == TargetObject || collision.gameObject.tag == TargetObject) {
            TargetCollision = true;
            TargetCollisionEnter = true;
            TargetCollisionExit = false;
            TargetObjectInfo = collision.gameObject;
            OnTargetCollisionEnter.Invoke();
        } else if (TargetObject == "") {
            TargetObjectInfo = collision.gameObject;
            OnTargetCollisionEnter.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.name == TargetObject || collision.gameObject.tag == TargetObject) {
            TargetCollision = false;
            TargetCollisionEnter = false;
            TargetCollisionExit = true;
            TargetObjectInfo = collision.gameObject;
            OnTargetCollisionExit.Invoke();
            CollisionCount = 0;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == TargetObject || other.gameObject.tag == TargetObject) {
            TargetTrigger = true;
            TargetTriggerEnter = true;
            TargetTriggerExit = false;
            TargetObjectInfo = other.gameObject;
            OnTargetTriggerEnter.Invoke();
        } else {
            TargetObjectInfo = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name == TargetObject || other.gameObject.tag == TargetObject) {
            TargetTrigger = false;
            TargetTriggerEnter = false;
            TargetTriggerExit = true;
            TargetObjectInfo = other.gameObject;
            OnTargetTriggerExit.Invoke();
            CollisionCount = 0;
        }
    }

    private void OnCollisionStay(Collision collision) {
        if(collision.gameObject.name == TargetObject || collision.gameObject.tag == TargetObject) {
            CollisionCount++;
        }
    }
}
