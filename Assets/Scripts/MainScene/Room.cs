using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Room : MonoBehaviour {
    private bool solved;

    public bool Solved {
        get => solved;
        set {
            solved = value;
            if (solved && RoomNumber != 3)
                GameManager.ActivateCorridor(CorridorToActivate);
        }
    }

    public int RoomNumber { get; set; }

    public int CorridorToActivate { get; set; }

    public GameManager GameManager;

    public void BringBallBack(GameObject ballToReturn, Vector3 origin) {
        Rigidbody rb = ballToReturn.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.velocity = Vector3.zero;
        ballToReturn.transform.position = origin;
        ballToReturn.transform.rotation = Quaternion.identity;
        rb.freezeRotation = false;
    }
}
