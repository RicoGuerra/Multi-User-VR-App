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
        [SerializeField] private Camera _VRInputSource;
        [SerializeField] private Camera _nonVRInputSource;

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
                    c.worldCamera = _VRInputSource;
                }
                _keyboard.SetActive(true);
            } else {
                foreach (Canvas c in _menuCanvas) {
                    c.worldCamera = _nonVRInputSource;
                }
            }
        }

        public void ToggleControllerPic(GameObject picture) {
            if (picture.activeInHierarchy) {
                picture.SetActive(false);
            } else {
                picture.SetActive(true);
            }
        }

        public void StartAnimation(Animator a) {
            if (!a.enabled) {
                a.enabled = true;
            } else {
                a.Play(0);
            }
        }

    }
}
