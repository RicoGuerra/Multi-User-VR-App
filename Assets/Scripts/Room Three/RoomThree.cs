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
        //for (int i = 0; i < ThrowableBalls.Length; i++) {
        //    throwableBallOrigins[i] = ThrowableBalls[i].transform.position;
        //}
    }

    void Update() {
        if (Platform.TargetCollision) {
            if (callCount < 300) {
                callCount++;
            } else {
                callCount = 0;
                BringBallBack(BowlingBall, BowlingBallPosition);
            }
        }
        IfPinsMove();
        for (int i = 0; i < ThrowableBalls.Length; i++) {
            if (ThrowableBalls[i].transform.position.y < -15f) {
                BringBallBack(ThrowableBalls[i], throwableBallOrigins[i].position);
            }
        }

        if (_pins.Count == 0 && !Solved) {
            Solved = true;
            GameManager.GameWon();
        }
    }

    private void IfPinsMove() {
        //distance = GameObject.FindGameObjectWithTag("Pin").GetComponent<Distance>(); //testen ob mit array funktioniert
        _pins = GameObject.FindGameObjectsWithTag("Pin").ToList();
        if (_pins == null)
            return;

        if (_pins.TrueForAll(IsMoving)) {
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
}
