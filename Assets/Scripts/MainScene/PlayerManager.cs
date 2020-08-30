using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR;
using System.Linq;
using Random = System.Random;
using System.Collections.Generic;
using Assets.Scripts.MainScene;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerManager : NetworkBehaviour {

    [SyncVar] public int PlayerID; /*{ get; private set; }*/
    public GameObject Avatar;
    public TextMesh NameTag;
    [SyncVar] public string PlayerName; /*{ get; set; }*/
    [SyncVar] public Color PlayerColor; /*{ get; set; }*/
    [SyncVar] public bool ComfortMode; /*{ get; set; }*/

    [SerializeField] private GameObject _playerCamera; // Zum testen wird die NonVRCamera des DebugModes zugeteilt
    [SerializeField] private Teleporter _teleporting;
    [SerializeField] private Behaviour[] componentsToDisable;
    [SerializeField] private List<Behaviour> disableWhenPaused;
    [SerializeField] private GameObject _pauseViewVR;

    public void Start() {
        RpcReadData();
        CmdReadData();
        PlayerSetup();
    }

    public void Update() {
        PauseMenu();
    }

    public void PlayerSetup() {
        if (!isLocalPlayer) {
            foreach (Behaviour b in componentsToDisable) {
                b.enabled = false;
            }
        }
        SetNameTag();
        SetMode();
        SetPlayerType();
    }

    private void SetPlayerType() {
        if (!isLocalPlayer) return;
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.SetLocalPlayerType(this);
    }

    private void SetMode() {
        if (ComfortMode && XRDevice.isPresent) {
            _teleporting.enabled = true;
            GetComponent<PlayerMovement>().enabled = false;
        }
    }

    private void SetNameTag() {
        Avatar.GetComponent<Renderer>().material.color = PlayerColor;
        PlayerID = netId.GetHashCode();
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
                if (ComfortMode && b.Equals(GetComponent<PlayerMovement>())) {
                    b.enabled = false;
                } else {
                    b.enabled = !Pause.Paused;
                }
            }
            if (XRDevice.isPresent)
                _pauseViewVR.SetActive(Pause.Paused);
        }
    }

    [ClientRpc]
    public void RpcReadData() {
        LoadMenuData load = new LoadMenuData(gameObject);
        load.LoadData();
    }

    [Command]
    public void CmdReadData() {
        LoadMenuData load = new LoadMenuData(gameObject);
        load.LoadData();
    }

    public void ReadData() {
        LoadMenuData load = new LoadMenuData(gameObject);
        load.LoadData();
    }

    [Command]
    public void CmdObjectAuthority(GameObject obj) {
        obj.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }

    [ClientRpc]
    public void RpcObjectAuthority(GameObject obj) {
        obj.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }

    [Command]
    public void CmdRemoveAuthority(GameObject obj) {
        obj.GetComponent<NetworkIdentity>().RemoveClientAuthority(obj.GetComponent<NetworkIdentity>().clientAuthorityOwner);
    }

    [ClientRpc]
    public void RpcRemoveAuthority(GameObject obj) {
        obj.GetComponent<NetworkIdentity>().RemoveClientAuthority(obj.GetComponent<NetworkIdentity>().clientAuthorityOwner);
    }

    public GameObject GetCamera() {
        return _playerCamera;
    }
}
