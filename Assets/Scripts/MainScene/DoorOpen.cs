using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.MainScene {
    public class DoorOpen : MonoBehaviour {
        public bool BallGoalHit { get; private set; }
        public UnityEvent onBallInGoal;

        private void OnTriggerEnter(Collider other) {
            Debug.Log("Colliding Object = " + other.name);
            if (other.name == "RollerBall") {
                BallGoalHit = true;
                GetComponentInParent<RoomOne>().Solved = true;
            }
        }
    }
}
