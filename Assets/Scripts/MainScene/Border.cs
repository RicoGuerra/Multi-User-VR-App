using UnityEngine;

public class Border : MonoBehaviour {

    private BorderManager _borderManager;
    /// <summary>
    /// Type = 0 >> regular border, no special treatment
    /// Type = 1 >> border that ignores some objects
    /// Type = 2 >> border that gets deactivated if a specific thing happens
    /// </summary>
    [SerializeField] public int BorderType { get; private set; }

    void Start() {
        _borderManager = GetComponentInParent<BorderManager>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "PassivePlayer" && BorderType == 1) {
            _borderManager.IgnoreObject(collision.collider, GetComponent<Collider>());
        }
    }
}
