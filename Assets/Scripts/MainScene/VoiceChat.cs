using UnityEngine;
using UnityEngine.UI;
using agora_gaming_rtc;
using Valve.VR;

namespace Assets.Scripts.MainScene {

    public class VoiceChat : MonoBehaviour {
        private InputField mChannelNameInputField;
        private Text mShownMessage;
        private Text versionText;
        private Button joinChannel;
        private Button leaveChannel;
        public Button muteText;
        public SteamVR_Action_Boolean MuteButtonVR;

        private IRtcEngine mRtcEngine = null;

        [SerializeField]
        private string appId = "";

        void Awake() {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 30;
        }

        void Start() {
            mRtcEngine = IRtcEngine.GetEngine(appId);
            JoinChannel();

            mRtcEngine.SetLogFilter(LOG_FILTER.INFO);

            mRtcEngine.SetChannelProfile(CHANNEL_PROFILE.CHANNEL_PROFILE_COMMUNICATION);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.M) || MuteButtonVR.GetStateDown(SteamVR_Input_Sources.Any)) {
                MuteButtonTapped();
            }
        }

        public void JoinChannel() {
            string channelName = "Bachelorarbeit";

            Debug.Log(string.Format("tap joinChannel with channel name {0}", channelName));

            if (string.IsNullOrEmpty(channelName)) {
                return;
            }

            mRtcEngine.JoinChannel(channelName, "extra", 0);
        }

        public void LeaveChannel() {

            mRtcEngine.LeaveChannel();
            string channelName = mChannelNameInputField.text.Trim();
            Debug.Log(string.Format("left channel name {0}", channelName));
        }

        void OnApplicationQuit() {
            if (mRtcEngine != null) {
                IRtcEngine.Destroy();
            }
        }

        public string getSdkVersion() {
            string ver = IRtcEngine.GetSdkVersion();
            if (ver == "2.9.1.45") {
                ver = "2.9.2";  // A conversion for the current internal version#
            } else {
                if (ver == "2.9.1.46") {
                    ver = "2.9.2.1";  // A conversion for the current internal version#
                }
            }
            return ver;
        }

        bool isMuted = false;
        void MuteButtonTapped() {
            string labeltext = isMuted ? "Unmuted" : "Muted";
            Text label = muteText.GetComponentInChildren<Text>();
            if (label != null) {
                label.text = labeltext;
            }
            isMuted = !isMuted;
            mRtcEngine.EnableLocalAudio(!isMuted);
            Debug.Log("__Audio muted == " + isMuted + "__");
        }
    }
}
