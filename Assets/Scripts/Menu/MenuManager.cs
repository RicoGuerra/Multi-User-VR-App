using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;

namespace Assets.Scripts.Menu {
    // Managing properties of the menuScene
    public class MenuManager : MonoBehaviour {

        [SerializeField] private Canvas[] _menuCanvas;
        [SerializeField] private GameObject _keyboard;

        public Camera VRInputSource;
        public Camera NonVRInputSource;

        private void Start() {
            SetEventSystem();
            SetupCanvasInput();
        }

        private static void SetEventSystem() {
            if (!XRDevice.isPresent) {
                GameObject.Find("EventSystem").SetActive(true);
                GameObject.Find("VRInputModule").SetActive(false);
            } else {
                GameObject.Find("EventSystem").SetActive(false);
                GameObject.Find("VRInputModule").SetActive(true);
            }
        }

        private void SetupCanvasInput() {
            if (XRDevice.isPresent) {
                foreach (Canvas c in _menuCanvas) {
                    c.worldCamera = VRInputSource;
                }
                _keyboard.SetActive(true);
            } else {
                foreach (Canvas c in _menuCanvas) {
                    c.worldCamera = NonVRInputSource;
                }
            }
        }
    }
}
