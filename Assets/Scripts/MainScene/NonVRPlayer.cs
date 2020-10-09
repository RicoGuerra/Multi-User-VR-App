using UnityEngine;

namespace Assets.Scripts.MainScene {

    public class NonVRPlayer : PlayerManager {

        private GameObject _activeSpawn;

        private void Start() {
            base.Start();
        }

        private void Update() {
            if (transform.position.y <= -5.0f) {
                _activeSpawn = GameObject.FindGameObjectWithTag("NVRSpawn");
                transform.position = _activeSpawn.transform.position;
            }
            base.Update();
        }

    }
}
