using UnityEngine;

public class CheckTargetCollision : MonoBehaviour {

    public bool TargetCollision { get; private set; }
    public bool TargetCollisionEnter { get; private set; }
    public bool TargetCollisionExit { get; private set; }
    public bool TargetTrigger { get; private set; }
    public bool TargetTriggerEnter { get; private set; }
    public bool TargetTriggerExit { get; private set; }
    public string TargetObject;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == TargetObject) {
            TargetCollision = true;
            TargetCollisionEnter = true;
            TargetCollisionExit = false;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.name == TargetObject) {
            TargetCollision = false;
            TargetCollisionEnter = false;
            TargetCollisionExit = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == TargetObject) {
            TargetTrigger = true;
            TargetTriggerEnter = true;
            TargetTriggerExit = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == TargetObject) {
            TargetTrigger = false;
            TargetTriggerEnter = false;
            TargetTriggerExit = true;
        }
    }
}
