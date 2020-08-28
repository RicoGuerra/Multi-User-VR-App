using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BorderManager : MonoBehaviour {

    private List<Collider> _allBorders;
    [SerializeField] private List<Collider> _roomOneBorders;
    [SerializeField] private List<Collider> _ignoreByDefault;
    // Start is called before the first frame update
    void Start() {
        _allBorders = GetComponentsInChildren<Collider>().ToList();
        foreach (Collider c0 in _ignoreByDefault) {
            foreach (Collider c1 in _allBorders) {
                IgnoreObject(c0, c1, true);
            }
        }
    }

    public void IgnoreObject(Collider objectToIgnore, Collider border, bool ignoring) {
        Physics.IgnoreCollision(objectToIgnore, border, ignoring);
    }
}
