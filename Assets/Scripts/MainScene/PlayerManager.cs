using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using Random = System.Random;

public class PlayerManager : NetworkBehaviour {

    public int PlayerID { get; private set; }
    public GameObject Avatar;
    public TextMesh NameTag;

    public string PlayerName { get; set; }
    public Color PlayerColor { get; set; }
    public bool ComfortMode { get; set; }
    [SerializeField]
    private Behaviour[] componentsToDisable;

    void Start() {
        ReadData();
        PlayerSetup();
    }

    void Update() {

    }

    public void PlayerSetup() {
        if (!isLocalPlayer) {
            for (int i = 0; i < componentsToDisable.Length; i++) {
                componentsToDisable[i].enabled = false;
            }
        }
        Random rnd = new Random();
        Avatar.GetComponent<Renderer>().material.color = PlayerColor;
        PlayerID = rnd.Next();
        if (PlayerName.All(char.IsWhiteSpace)) {
            name = "Player" + PlayerID;
            PlayerName = name;
        } else {
            name = PlayerName;
        }
        NameTag.text = name;
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
