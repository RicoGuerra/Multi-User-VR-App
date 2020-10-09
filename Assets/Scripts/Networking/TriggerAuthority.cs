using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class TriggerAuthority : MonoBehaviour {

    private PlayerManager _player;
    private Interactable _thisObject;

    private void Start() {
        _thisObject = GetComponent<Interactable>();
    }

    public void AuthorityRequest() {
        if (_thisObject.attachedToHand == null) {
            _player = _thisObject.hoveringHand.transform.root.gameObject.GetComponent<PlayerManager>();
        } else {
            _player = _thisObject.attachedToHand.transform.root.gameObject.GetComponent<PlayerManager>();
        }
        _player.CmdObjectAuthority(gameObject);
        _player.RpcObjectAuthority(gameObject);
    }

    public void AuthorityRequest(GameObject obj) {
        if (_thisObject.attachedToHand == null) {
            _player = _thisObject.hoveringHand.transform.root.gameObject.GetComponent<PlayerManager>();
        } else {
            _player = _thisObject.attachedToHand.transform.root.gameObject.GetComponent<PlayerManager>();
        }
        _player.CmdObjectAuthority(obj);
        _player.RpcObjectAuthority(obj);
    }

    public void AuthorityRemoval() {
        if (_player != null) {
            _player.CmdRemoveAuthority(gameObject);
            _player.RpcRemoveAuthority(gameObject);
        }
    }

    public void AuthorityRemoval(GameObject obj) {
        if (_player != null) {
            _player.CmdRemoveAuthority(obj);
            _player.RpcRemoveAuthority(obj);
        }
    }
}
