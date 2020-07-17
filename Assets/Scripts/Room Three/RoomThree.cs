using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomThree : Room {

    private int callCount;
    private Vector3 destroyingObjectPosition;
    private Vector3 BowlingBallPosition;
    private Vector3[] throwableBallOrigins;

    public CheckTargetCollision Platform;
    public Collider[] Barriere;
    public Distance Distance;
    public GameObject BowlingBall;
    public GameObject[] ThrowableBalls;

    // Start is called before the first frame update
    void Start() {
        BowlingBallPosition = BowlingBall.transform.position;
        throwableBallOrigins = new Vector3[3];
        for (int i = 0; i < ThrowableBalls.Length; i++) {
            throwableBallOrigins[i] = ThrowableBalls[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Platform.TargetCollision) {
            if (callCount < 300) {
                callCount++;
            } else {
                callCount = 0;
                BringBallBack(BowlingBall, BowlingBallPosition);
            }
        }
        if (Distance.IsMoving) {
            IgnoreBarrier(true);
        } else {
            IgnoreBarrier(false);
        }
        for (int i = 0; i < ThrowableBalls.Length; i++) {
            if (ThrowableBalls[i].transform.position.y < -15f) {
                BringBallBack(ThrowableBalls[i], throwableBallOrigins[i]);
            }
        }
    }

    private void IgnoreBarrier(bool ignore) {
        foreach (Collider collider in Barriere) {
            foreach (GameObject ball in ThrowableBalls) {
                Physics.IgnoreCollision(ball.GetComponentInChildren<Collider>(), collider, ignore);
            }
        }
    }

    private void DestroyBall() {
        BowlingBall.transform.rotation = new Quaternion(0, 0, 0, 0);
        BowlingBall.transform.position = BowlingBallPosition;
    }
}
