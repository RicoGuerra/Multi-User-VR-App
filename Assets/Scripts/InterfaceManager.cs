using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class InterfaceManager : MonoBehaviour {

    public bool Activated { get; set; }

    void Start() {
        Activated = false;
    }

    //private void OnTriggerStay(Collider other) {
    //    if (other.tag == "Hand" && other.GetComponent<Hand>().grabGripAction.state) {//doesnt work
    //        Activated = true;
    //    } else if (other.tag == "Hand" && !other.GetComponent<Hand>().grabGripAction.state) {
    //        Activated = false;
    //    }
    //}
}
