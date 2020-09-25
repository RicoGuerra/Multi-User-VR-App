using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCountDown : MonoBehaviour {

    [SerializeField] private float _timer;

    [SerializeField]
    private TextMesh _timerText;

    void Update() {
        if (gameObject.activeInHierarchy) {
            if(_timer > 0) {
                _timer -= Time.deltaTime;
                _timerText.text = "" + Mathf.Round(_timer);
            } else {
                GameManager.EndGame();
            }
        }
    }
}
