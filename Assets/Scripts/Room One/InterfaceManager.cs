using UnityEngine;
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
        TogglePlayerMovement(!Activated);
    }

    public void Deactivate() {
        Activated = false;
        TogglePlayerMovement(!Activated);
    }

    public void TogglePlayerMovement(bool onOff) {
        if(PlayerOnInterface.ComfortMode) {
            PlayerOnInterface.transform.root.GetComponentInChildren<Teleporter>(true).enabled = onOff;
        } else {
            PlayerOnInterface.transform.root.GetComponentInChildren<PlayerMovement>(true).enabled = onOff;
        }
    }
}
