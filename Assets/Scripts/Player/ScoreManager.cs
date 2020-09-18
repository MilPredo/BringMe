using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ScoreManager : NetworkBehaviour {


    [SyncVar] private int score;
    [SyncVar] private int index;
    private TMPro.TextMeshProUGUI playerScoreBoard;

    public override void OnStartServer() {
        this.index = connectionToClient.connectionId;
    }

    public void AddScore() {
        this.score += 1;
    }

    public int Index { get; }

    public int Score { get; }

    public void SetScoreBoard(TMPro.TextMeshProUGUI scoreBoard) {
        this.playerScoreBoard = scoreBoard;
    }

    private void OnGUI() {
        // GUI.Box( new Rect( 200f + ( index + 210 ), 10f, 200f, 25f ), $"P{ index }: { score.ToString("000") }" );
        if ( this.playerScoreBoard != null )
            this.playerScoreBoard.text = $"{ gameObject.name } : { this.score.ToString() }";
    }
}
