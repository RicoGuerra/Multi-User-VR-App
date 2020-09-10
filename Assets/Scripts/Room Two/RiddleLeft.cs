using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RiddleLeft : NetworkBehaviour {

    [SyncVar]
    public bool RiddleSolved;
    public RoomTwo RoomTwo;

    [ClientRpc]
    public void RpcSolvedRiddel() {
        RiddleSolved = true;
        RoomTwo.LeftSolved = RiddleSolved;
        RoomTwo.LeftSuccessfullySolved();
    }

    [Command]
    public void CmdSolvedRiddel() {
        RiddleSolved = true;
        RoomTwo.LeftSolved = RiddleSolved;
        RoomTwo.LeftSuccessfullySolved();
    }
}
