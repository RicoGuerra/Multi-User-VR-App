using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RoomTwo : Room {

    public List<GameObject> RiddleRight;

    private int[] _riddleLeftOrder;
    private int _buttonCounter;
    private bool _rightSolved;
    private bool _leftSolved;
    private AudioSource _successSound;

    private void Start() {
        _successSound = GetComponent<AudioSource>();
        _riddleLeftOrder = new int[4];
        _buttonCounter = 0;
        CorridorToActivate = 1;
    }

    void Update() {
        if (_rightSolved && _leftSolved) {
            Solved = true;
        }
        if (RiddleRight.TrueForAll(TargetCollision)) {
            if (!_rightSolved) {
                _successSound.Play();
                _rightSolved = true;
                GameObject.Find("ExitRight").SetActive(false);
            }
        }
        KeepCubesOnTable();
    }

    public void CheckRiddleLeft(int buttonIndex) {
        _riddleLeftOrder[_buttonCounter] = buttonIndex;
        _buttonCounter++;
        if (_riddleLeftOrder[0] == 2 && _riddleLeftOrder[1] == 1 && _riddleLeftOrder[2] == 0 && _riddleLeftOrder[3] == 4 && !_leftSolved) {
            _successSound.Play();
            _leftSolved = true;
            GameObject.Find("ExitLeft").SetActive(false);
        } else if (_buttonCounter == 4) {
            _buttonCounter = 0;
            for (int i = 0; i < _riddleLeftOrder.Length; i++)
                _riddleLeftOrder[i] = 0;
        }
    }

    public void Entrance(string enter) {
        GameObject entrance = GameObject.Find(enter);
        CheckTargetCollision target = entrance.GetComponent<CheckTargetCollision>();
        if (target.TargetTriggerExit && target.TargetObjectInfo.transform.position.z < entrance.transform.position.z) {
            entrance.GetComponent<MeshRenderer>().enabled = true;
            entrance.GetComponent<Collider>().isTrigger = false;
        }
    }

    private void KeepCubesOnTable() {
        GameObject cubeOnFloor = RiddleRight.Find(c => CubeOnFloor(c));
        if (cubeOnFloor != null) {
            cubeOnFloor.transform.localPosition = new Vector3(0.1f, 0.5f, -0.5f);
            cubeOnFloor.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private bool TargetCollision(GameObject cube) {
        return cube.GetComponent<CheckTargetCollision>().TargetCollision;
    }

    private bool CubeOnFloor(GameObject cube) {
        return cube.transform.localPosition.y <= -0.85f ? true : false;
    }


}
