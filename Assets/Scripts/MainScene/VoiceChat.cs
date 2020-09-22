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

        //void Awake() {
        //    QualitySettings.vSyncCount = 0;
        //    Application.targetFrameRate = 30;
        //}

        void Start() {
            mRtcEngine = IRtcEngine.GetEngine(appId);
            JoinChannel();
            mRtcEngine.OnJoinChannelSuccess += (string channelName, uint uid, int elapsed) => {
                string joinSuccessMessage = string.Format("joinChannel callback uid: {0}, channel: {1}, version: {2}", uid, channelName, getSdkVersion());
                Debug.Log(joinSuccessMessage);
                mShownMessage.GetComponent<Text>().text = (joinSuccessMessage);
                muteText.enabled = true;
            };

            mRtcEngine.OnLeaveChannel += (RtcStats stats) => {
                string leaveChannelMessage = string.Format("onLeaveChannel callback duration {0}, tx: {1}, rx: {2}, tx kbps: {3}, rx kbps: {4}", stats.duration, stats.txBytes, stats.rxBytes, stats.txKBitRate, stats.rxKBitRate);
                Debug.Log(leaveChannelMessage);
                mShownMessage.GetComponent<Text>().text = (leaveChannelMessage);
                muteText.enabled = false;
                // reset the mute button state
                if (isMuted) {
                    MuteButtonTapped();
                }
            };

            mRtcEngine.OnUserJoined += (uint uid, int elapsed) => {
                string userJoinedMessage = string.Format("onUserJoined callback uid {0} {1}", uid, elapsed);
                Debug.Log(userJoinedMessage);
            };

            mRtcEngine.OnUserOffline += (uint uid, USER_OFFLINE_REASON reason) => {
                string userOfflineMessage = string.Format("onUserOffline callback uid {0} {1}", uid, reason);
                Debug.Log(userOfflineMessage);
            };

            mRtcEngine.OnVolumeIndication += (AudioVolumeInfo[] speakers, int speakerNumber, int totalVolume) => {
                if (speakerNumber == 0 || speakers == null) {
                    Debug.Log(string.Format("onVolumeIndication only local {0}", totalVolume));
                }

                for (int idx = 0; idx < speakerNumber; idx++) {
                    string volumeIndicationMessage = string.Format("{0} onVolumeIndication {1} {2}", speakerNumber, speakers[idx].uid, speakers[idx].volume);
                    Debug.Log(volumeIndicationMessage);
                }
            };

            mRtcEngine.OnUserMutedAudio += (uint uid, bool muted) => {
                string userMutedMessage = string.Format("onUserMuted callback uid {0} {1}", uid, muted);
                Debug.Log(userMutedMessage);
            };

            mRtcEngine.OnWarning += (int warn, string msg) => {
                string description = IRtcEngine.GetErrorDescription(warn);
                string warningMessage = string.Format("onWarning callback {0} {1} {2}", warn, msg, description);
                Debug.Log(warningMessage);
            };

            mRtcEngine.OnError += (int error, string msg) => {
                string description = IRtcEngine.GetErrorDescription(error);
                string errorMessage = string.Format("onError callback {0} {1} {2}", error, msg, description);
                Debug.Log(errorMessage);
            };

            mRtcEngine.OnRtcStats += (RtcStats stats) => {
                string rtcStatsMessage = string.Format("onRtcStats callback duration {0}, tx: {1}, rx: {2}, tx kbps: {3}, rx kbps: {4}, tx(a) kbps: {5}, rx(a) kbps: {6} users {7}",
                    stats.duration, stats.txBytes, stats.rxBytes, stats.txKBitRate, stats.rxKBitRate, stats.txAudioKBitRate, stats.rxAudioKBitRate, stats.userCount);
                Debug.Log(rtcStatsMessage);

                int lengthOfMixingFile = mRtcEngine.GetAudioMixingDuration();
                int currentTs = mRtcEngine.GetAudioMixingCurrentPosition();

                string mixingMessage = string.Format("Mixing File Meta {0}, {1}", lengthOfMixingFile, currentTs);
                Debug.Log(mixingMessage);
            };

            mRtcEngine.OnAudioRouteChanged += (AUDIO_ROUTE route) => {
                string routeMessage = string.Format("onAudioRouteChanged {0}", route);
                Debug.Log(routeMessage);
            };

            mRtcEngine.OnRequestToken += () => {
                string requestKeyMessage = string.Format("OnRequestToken");
                Debug.Log(requestKeyMessage);
            };

            mRtcEngine.OnConnectionInterrupted += () => {
                string interruptedMessage = string.Format("OnConnectionInterrupted");
                Debug.Log(interruptedMessage);
            };

            mRtcEngine.OnConnectionLost += () => {
                string lostMessage = string.Format("OnConnectionLost");
                Debug.Log(lostMessage);
            };

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
