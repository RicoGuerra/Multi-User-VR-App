using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RoomThree : Room {

    private int callCount;
    private Vector3 destroyingObjectPosition;
    private Vector3 BowlingBallPosition;
    [SerializeField] private Transform[] throwableBallOrigins;
    private Distance distance;
    private List<GameObject> _pins;

    public CheckTargetCollision Platform;
    public Collider[] Barriere;
    public GameObject BowlingBall;
    public GameObject[] ThrowableBalls;

    void Start() {
        RoomNumber = 3;
        BowlingBallPosition = BowlingBall.transform.position;
        Physics.IgnoreCollision(BowlingBall.GetComponentInChildren<Collider>(), Barriere[8], true);
    }

    void Update() {
        if (Platform.TargetCollision) {
            if (callCount < 600) {
                callCount++;
            } else {
                callCount = 0;
                BringBallBack(BowlingBall, BowlingBallPosition);
            }
        }
        IfPinsMove();

        if (BowlingBall.transform.position.y < -10) {
            BringBallBack(BowlingBall, BowlingBallPosition);
        }

        if (_pins != null && _pins.Count == 0 && !Solved) {
            Solved = true;
            GameManager.GameWon();
        }
        for (int i = 0; i < ThrowableBalls.Length; i++) {
            if (ThrowableBalls[i].transform.position.y < -15f) {
                BringBallBack(ThrowableBalls[i], throwableBallOrigins[i].position);
            }
        }
    }

    private void IfPinsMove() {
        _pins = GameObject.FindGameObjectsWithTag("Pin").ToList();
        if (_pins == null) {
            return;
        }
        if ((_pins.TrueForAll(IsMoving) || _pins.Find(p => IsMoving(p))) && _pins.Count > 0) {
            IgnoreBarrier(true);
        } else {
            IgnoreBarrier(false);
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

    private bool IsMoving(GameObject pin) {
        return pin.GetComponent<Distance>().IsMoving;
    }

    public void BringSmallBallBack(GameObject ball) {
        Vector3 orig;
        for (int i = 0; i < ThrowableBalls.Length; i++) {
            if (ball == ThrowableBalls[i]) {
                orig = throwableBallOrigins[i].position;
                BringBallBack(ball, orig);
                return;
            }
        }
    }
}