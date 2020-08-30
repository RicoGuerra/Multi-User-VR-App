using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Valve.VR.InteractionSystem;

public class TriggerAuthority : MonoBehaviour {

    private PlayerManager _player;
    private Interactable _thisObject;

    private void Start() {
        _thisObject = GetComponent<Interactable>();
    }

    public void AuthorityRequest() {
        _player = _thisObject.attachedToHand.transform.root.gameObject.GetComponent<PlayerManager>();
        _player.CmdObjectAuthority(gameObject);
        _player.RpcObjectAuthority(gameObject);
    }

    public void AuthorityRemoval() {
        if (_player != null) {
            _player.CmdRemoveAuthority(gameObject);
            _player.RpcRemoveAuthority(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        //coming later
    }

    private void OnCollisionExit(Collision collision) {
        //coming later
    }

}
