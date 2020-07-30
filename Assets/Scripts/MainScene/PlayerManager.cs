using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR;
using System.Linq;
using Random = System.Random;
using System.Collections.Generic;
using Assets.Scripts.MainScene;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerManager : NetworkBehaviour {

    public int PlayerID { get; private set; }
    public GameObject Avatar;
    public TextMesh NameTag;
    public GameObject Teleporting;
    public string PlayerName { get; set; }
    public Color PlayerColor { get; set; }
    public bool ComfortMode { get; set; }

    [SerializeField] private Behaviour[] componentsToDisable;
    [SerializeField] private List<Behaviour> disableWhenPaused;

    public void Start() {
        ReadData();
        PlayerSetup();
    }

    public void Update() {
        PauseMenu();
    }

    public void PlayerSetup() {
        if (!isLocalPlayer) {
            for (int i = 0; i < componentsToDisable.Length; i++) {
                componentsToDisable[i].enabled = false;
            }
        }
        SetNameTag();
        SetMode();
    }

    private void SetMode() {
        if (ComfortMode && XRDevice.isPresent) {
            Instantiate(Teleporting);
            GetComponent<PlayerMovement>().enabled = false;
        }
    }

    private void SetNameTag() {
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

    public void PauseMenu() {
        if (isLocalPlayer) {
            if (Pause.Paused != disableWhenPaused.First().enabled) return;
            foreach (Behaviour b in disableWhenPaused) {
                b.enabled = !Pause.Paused;
            }
        }
    }

    public void ReadData() {
        LoadMenuData load = new LoadMenuData(gameObject);
        load.LoadData();
    }

    public GameObject GetCamera() {
        return GameObject.Find("VRCamera");
    }
}
