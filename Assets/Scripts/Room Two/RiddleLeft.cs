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

    public void IsSolved() {
        RiddleSolved = true;
        RoomTwo.LeftSolved = RiddleSolved;
        RoomTwo.LeftSuccessfullySolved();
    }
}
