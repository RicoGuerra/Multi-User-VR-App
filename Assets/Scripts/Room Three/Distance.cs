﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour {
    [Tooltip("Objekt von dem sich wegbewegt wird")]
    public GameObject Obj1;
    private GameObject Obj2;
    public float distance;
    public float x = 3f;
    public float z = 3f;
    private Vector3 origin;
    private bool safeDistance;
    public float threshold;
    public bool ShallNotFall;
    public bool IsMoving { get; private set; }
    [Header("Border")]
    public float North;
    public float East;
    public float South;
    public float West;

    // Start is called before the first frame update
    void Start() {
        Obj2 = gameObject;
        origin = Obj2.transform.position;
        threshold = 0.15f;
        if (ShallNotFall) {
            gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update() {
        distance = Vector3.Distance(Obj1.transform.position, Obj2.transform.position);
        KeepDistance();
    }

    private void KeepDistance() {
        if (distance < 3.5) {
            IsMoving = true;
            safeDistance = false;
            AwayFromPlayer();
        } else if (distance >= 5.5f) {
            safeDistance = true;
            BackToOrigin();
        }
    }

    private void BackToOrigin() {
        if (safeDistance && Obj2.transform.position != origin) {
            if (Obj2.transform.position.x < origin.x && Obj2.transform.position.x < origin.x - threshold) {
                Obj2.transform.position += new Vector3(x * Time.deltaTime, 0, 0);
            } else if (Obj2.transform.position.x > origin.x && Obj2.transform.position.x > origin.x + threshold) {
                Obj2.transform.position -= new Vector3(x * Time.deltaTime, 0, 0);
            } else if (Obj2.transform.position.x == origin.x) {
                return;
            } else {
                IsMoving = false;
            }
            if (Obj2.transform.position.z < origin.z && Obj2.transform.position.z < origin.z - threshold) {
                Obj2.transform.position += new Vector3(0, 0, z * Time.deltaTime);
            } else if (Obj2.transform.position.z > origin.z && Obj2.transform.position.z > origin.z + threshold) {
                Obj2.transform.position -= new Vector3(0, 0, z * Time.deltaTime);
            } else if (Obj2.transform.position.z == origin.z) {
                return;
            } else {
                IsMoving = false;
            }
        }
    }

    private void AwayFromPlayer() {
        if (!safeDistance) {
            if (Obj2.transform.position.x > Obj1.transform.position.x && (Obj2.transform.position.x <= North || Obj2.transform.position.x >= South)) {
                Obj2.transform.position += new Vector3(x * Time.deltaTime, 0, 0);
            } else {
                Obj2.transform.position -= new Vector3(x * Time.deltaTime, 0, 0);
            }
            if (Obj2.transform.position.z > Obj1.transform.position.z && (Obj2.transform.position.z >= East || Obj2.transform.position.z <= West)) {
                Obj2.transform.position += new Vector3(0, 0, z * Time.deltaTime);
            } else {
                Obj2.transform.position -= new Vector3(0, 0, z * Time.deltaTime);
            }
        }
    }
}
