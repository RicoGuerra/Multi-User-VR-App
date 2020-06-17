using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private int PlayerID;
    public GameObject Avatar;
    public string PlayerName { get; set; }
    public Color PlayerColor { get; set; }
    public bool ComfortMode { get; set; }
    [SerializeField]
    private Behaviour[] componentsToDisable;

    void Start() {
        ReadData();
        Avatar.GetComponent<Renderer>().material.color = PlayerColor;
        name = PlayerName;
    }

    void Update() {

    }

    public void PlayerSetup() {

    }

    public void ReadData() {
        LoadMenuData load = new LoadMenuData(gameObject);
        load.LoadData();
    }

    public void ReceiveEndGameReq() {

    }

    public void SendEndGameReq() {

    }
}
