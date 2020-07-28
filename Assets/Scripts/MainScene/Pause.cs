using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using Valve.VR;

namespace Assets.Scripts.MainScene {

    public class Pause : MonoBehaviour {

        public static bool Paused { get; set; }

        [SerializeField] private SteamVR_Action_Boolean _pauseButton;

        private void Update() {
            if (XRDevice.isPresent && _pauseButton.GetStateDown(SteamVR_Input_Sources.Any)) {
                TogglePause();
            } else if (!XRDevice.isPresent && Input.GetKeyDown(KeyCode.Tab)) {
                TogglePause();
            }

            if (Paused && Input.GetKeyDown(KeyCode.Escape)) {
                Quit();
            }
        }

        public void TogglePause() {
            Paused = !Paused;
            transform.GetChild(0).gameObject.SetActive(Paused);
        }

        public void Quit() {
            Debug.Log("QUIT GAME FROM PAUSE MENU!!!");
            GameManager.EndGame();
        }
    }
}
