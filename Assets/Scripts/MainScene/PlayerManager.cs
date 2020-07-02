using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

    private int PlayerID;
    public GameObject Avatar;
    public string PlayerName { get; set; }
    public Color PlayerColor { get; set; }
    public bool ComfortMode { get; set; }
    [SerializeField]
    private Behaviour[] componentsToDisable;

    void Start() {
        PlayerSetup();
        ReadData();
        Avatar.GetComponent<Renderer>().material.color = PlayerColor;
        name = PlayerName;
    }

    void Update() {

    }

    public void PlayerSetup() {
        if (!isLocalPlayer) {
            for (int i = 0; i < componentsToDisable.Length; i++) {
                componentsToDisable[i].enabled = false;
            }
        }
    }

    public void ReadData() {
        LoadMenuData load = new LoadMenuData(gameObject);
        load.LoadData();
    }

    public void ReceiveEndGameReq() {

    }

    public void SendEndGameReq() {

    }

    public GameObject GetCamera() {
        return GameObject.Find("VRCamera");
    }
}
