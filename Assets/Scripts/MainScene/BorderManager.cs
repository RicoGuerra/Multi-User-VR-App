using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BorderManager : MonoBehaviour {

    private List<Collider> _allBorders;
    [SerializeField] private List<Collider> _roomOneBorders;
    [SerializeField] private List<Collider> _movingPlattformR1;
    [SerializeField] private List<Collider> _ignoreByDefault;

    void Start() {
        _allBorders = GetComponentsInChildren<Collider>().ToList();
        SetUpBorders();
        foreach (Collider c in _ignoreByDefault) {
            IgnoreObject(c, _allBorders, true);
        }
    }
    /// <summary>
    /// A number of borders ignore one specific object
    /// </summary>
    /// <param name="objectToIgnore"></param>
    /// <param name="borders"></param>
    /// <param name="ignoring"></param>
    public void IgnoreObject(Collider objectToIgnore, List<Collider> borders, bool ignoring = true) {
        foreach (Collider c in borders) {
            Physics.IgnoreCollision(objectToIgnore, c, ignoring);
        }
    }
    /// <summary>
    /// border ignores the collision of objectToIgnore
    /// </summary>
    /// <param name="objectToIgnore"></param>
    /// <param name="border"></param>
    /// <param name="ignoring"></param>
    public void IgnoreObject(Collider objectToIgnore, Collider border, bool ignoring = true) {
        Physics.IgnoreCollision(objectToIgnore, border, ignoring);
    }

    private void SetUpBorders() {
        foreach (Collider c in _allBorders) {
            c.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    public void DeactivateAllBorders() {
        foreach (Collider c in _allBorders)
            c.enabled = false;
    }

    public void DeactivateBorder(GameObject border) {
        border.SetActive(false);
    }
}
