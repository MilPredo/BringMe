using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ScoreManager : NetworkBehaviour {


    [SyncVar] private int score;
    [SyncVar] private int index;

    public override void OnStartServer() {
        this.index = connectionToClient.connectionId;
    }

    public void AddScore() {
        this.score += 1;
    }

    private void OnGUI() {
        GUI.Box( new Rect( 200f + ( index + 210 ), 10f, 200f, 25f ), $"P{ index }: { score.ToString("000") }" );
    }
}
