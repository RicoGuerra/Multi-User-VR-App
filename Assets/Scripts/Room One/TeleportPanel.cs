using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Assets.Scripts.Room_One {

    public class TeleportPanel : MonoBehaviour {

        [SerializeField] private Vector3 _teleportSpot;
        private bool _isTeleporting;
        private GameObject _teleportingObject;

        public void Teleport() {
            if (_isTeleporting) return;
            _teleportingObject = GetComponentInChildren<HoverButton>().lastHoveredHand.transform.root.gameObject;
            StartCoroutine(Move(_teleportingObject));
        }

        IEnumerator Move(GameObject teleportingObject) {
            _isTeleporting = true;

            SteamVR_Fade.Start(Color.black, 1.5f, true);

            yield return new WaitForSeconds(1.5f);
            _teleportSpot.y = teleportingObject.transform.position.y;
            teleportingObject.transform.position = _teleportSpot;

            SteamVR_Fade.Start(Color.clear, 1.5f, true);

            _isTeleporting = false;
        }
    }
}
