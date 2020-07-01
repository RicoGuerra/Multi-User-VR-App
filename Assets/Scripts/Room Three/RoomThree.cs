using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomThree : MonoBehaviour {

    private int callCount;
    private Vector3 destroyingObjectPosition;
    private Vector3 BowlingBallPosition;

    public BowlingPlatform Platform;
    public Collider[] Barriere;
    public Distance Distance;
    public GameObject BowlingBall;
    public GameObject[] ThrowableBalls;

    // Start is called before the first frame update
    void Start() {
        BowlingBallPosition = BowlingBall.transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (Platform.BallIsColliding && callCount == 0) {
            StartCoroutine(DestroyBall());
        }
        if (Distance.IsMoving) {
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

    private void DestroyObject(GameObject obj, bool spawnAgain) {
        if (spawnAgain) {
            destroyingObjectPosition = obj.transform.position;
        }
        Destroy(obj);
    }

    private void InstantiateObj(GameObject obj) {
        if (destroyingObjectPosition != Vector3.zero) {
            Instantiate(obj, destroyingObjectPosition, new Quaternion(0, 0, 0, 0));
            destroyingObjectPosition = Vector3.zero;
        } else {
            Instantiate(obj);
        }
    }

    private IEnumerator DestroyBall() {
        callCount++;
        Platform.BallIsColliding = false;
        yield return new WaitForSeconds(5);
        //Destroy(BowlingBall);
        BowlingBall.transform.rotation = new Quaternion(0, 0, 0, 0);
        BowlingBall.transform.position = BowlingBallPosition;
        callCount = 0;
    }
}
