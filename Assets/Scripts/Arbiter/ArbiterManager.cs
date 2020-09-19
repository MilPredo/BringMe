using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ArbiterManager : NetworkBehaviour {

    [SerializeField] List<Item> targetSelections;
    [SerializeField] private Item targetItem;
    [SerializeField] private TMPro.TextMeshProUGUI targetItemText;
    [SerializeField] private TMPro.TextMeshProUGUI itemsLeftToBringText;
    [SerializeField] private GameObject scoreBoardContent;
    [SerializeField] private GameObject scoreBoardContentTextPrefab;

    [SerializeField] private RoundManager roundManager;

    [SyncVar] private int itemsLeftToBring = 3;

    public int ItemsLeftToBring { 
        set {
            this.itemsLeftToBring = value;
            this.itemsLeftToBringText.text = $"ITEMS LEFT: { this.itemsLeftToBring.ToString() }";
        }
    }
    
    public override void OnStartServer() {
        base.OnStartServer();
        ChangeTargetItem();
        this.itemsLeftToBringText.text = $"ITEMS LEFT: { this.itemsLeftToBring.ToString() }";
    }

    public void ChangeTargetItem() {
        this.targetItem = targetSelections[ Random.Range( 0, targetSelections.Count ) ];
        this.targetItemText.text = $"TARGET: { this.targetItem.prefab.name.ToUpper() }";
        Debug.Log($"Changing Target Item: { this.targetItem.prefab.name }");
    }

    public void PickUpItem(GameObject player) {
        ScoreManager playerScoreManager = player.GetComponent<ScoreManager>();
        playerScoreManager.AddScore();

        // check if player is already added in score list
        bool isPlayerListed  = false;
        foreach (Transform child in this.scoreBoardContent.transform) {
            if ( child.gameObject.name == $"{ playerScoreManager.Index.ToString() }:{ player.name }" ) {
                Debug.Log($"Player { player.name } updating score.");
                isPlayerListed = true;
                break;
            }
            isPlayerListed = false;
        }

        if ( !isPlayerListed ) {
            // add player score to score board
            Debug.Log($"Player { player.name } added to score board.");
            
            GameObject playerScoreText  = Instantiate<GameObject>( this.scoreBoardContentTextPrefab );
            playerScoreText.name = $"{ playerScoreManager.Index.ToString() }:{ player.name }";
            playerScoreText.transform.SetParent( scoreBoardContent.transform, true );
            playerScoreText.transform.position = scoreBoardContent.transform.position;

            playerScoreManager.SetScoreBoard( playerScoreText.GetComponent<TMPro.TextMeshProUGUI>() );
        }
            
        ChangeTargetItem();
        this.itemsLeftToBring -= 1;
        this.itemsLeftToBringText.text = $"ITEMS LEFT: { this.itemsLeftToBring.ToString() }";
        if ( this.itemsLeftToBring == 0 ) {
            this.roundManager.StopRound();
        }
    }

    public string TargetItemName {
        get { return $"{ this.targetItem.prefab.name }(Clone)"; }
        private set {}
    }

}
