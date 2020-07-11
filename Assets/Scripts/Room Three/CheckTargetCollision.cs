using UnityEngine;

public class CheckTargetCollision : MonoBehaviour {

    public bool TargetCollision { get; private set; }
    public string TargetObject;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == TargetObject) {
            TargetCollision = true;
        } else {
            TargetCollision = false;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.name == TargetObject)
            TargetCollision = false;
    }
}
