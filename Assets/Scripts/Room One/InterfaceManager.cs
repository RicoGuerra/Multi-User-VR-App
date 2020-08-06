using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class InterfaceManager : MonoBehaviour {

    public bool Activated { get; set; }
    public PlayerManager PlayerOnInterface { get; private set; }

    void Awake() {
        Activated = false;
    }

    void Update() {
        if (GetComponentInChildren<Interactable>().hoveringHand != null)
            PlayerOnInterface = GetComponentInChildren<Interactable>().hoveringHand.transform.root.gameObject.GetComponent<PlayerManager>();
    }

    public void Activate() {
        Activated = true;
    }

    public void Deactivate() {
        Activated = false;
    }
}
