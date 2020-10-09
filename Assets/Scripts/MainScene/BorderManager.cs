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

    public void CorridorToggle(int option, int corridorIndex) {
        // option 0 >> beide borders, kein corridor
        // option 1 >> corridor, border vorn weg
        // option 2 >> corridor, border hinten weg
        if (option < 0 || option > 2 || corridorIndex > 1) {
            Debug.LogError("Ungültige Option für CorridorToggle");
            return;
        }
        List<Collider> corridorBorders = _allBorders.FindAll(c => c.name.Contains("Corridor" + corridorIndex));
        if (option == 0) {
            foreach (Collider c in corridorBorders)
                c.enabled = true;
        } else if (option == 1) {
            corridorBorders.Find(border => border.name.Contains("to")).enabled = false;
        } else if (option == 2) {
            corridorBorders.Find(border => border.name.Contains("from")).enabled = false;
            corridorBorders.Find(border => border.name.Contains("to")).enabled = true;
        }
    }

    public void DeactivateBorder(GameObject border) {
        border.SetActive(false);
    }
}
