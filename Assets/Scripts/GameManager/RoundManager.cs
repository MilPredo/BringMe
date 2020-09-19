using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour {
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject prepareRoundMenu;
    [SerializeField] private GameObject roundEndMenu;
    [SerializeField] private GameObject endGameResultMenu;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private TMPro.TextMeshProUGUI prepareCountdownText;
    [SerializeField] private TMPro.TextMeshProUGUI prepareMessageText;
    [SerializeField] private TMPro.TextMeshProUGUI timerText;

    // private ArbiterManager arbiterManager;

    private void Update() {
        if ( gameManager.isPlaying )
            this.gameManager.currentRoundTime -= Time.deltaTime;
        SetTimerTime( this.timerText, this.gameManager.currentRoundTime );
        if (this.gameManager.currentRoundTime < 0 && ( gameManager.isPlaying ) ) {
            EndRound();
            this.gameManager.currentPrepareTime -= Time.deltaTime;
            SetTimerTime( this.prepareCountdownText, this.gameManager.currentPrepareTime );
            if ( this.gameManager.currentPrepareTime < 0 && this.gameManager.isPlaying ) {
                StartRound();
            }
        }
    }

    private void StartRound() {
        // player.SetActive(true);
        // ui.SetActive(true);
        // player.transform.position = new Vector3(0f, 0.5f, 0f);
        prepareRoundMenu.SetActive(false);
        this.gameManager.currentRoundTime = this.gameManager.MaxRoundTime;
        this.gameManager.currentRound += 1;
        // arbiterManager.ItemsLeftToBring = this.gameManager.MaxRound - (this.gameManager.currentRound-1);
    }

    private void EndRound() {
        // deactivate game
        // player.SetActive(false);
        // ui.SetActive(false);
        if (this.gameManager.currentRound >= this.gameManager.MaxRound) {
            Debug.Log("Showing Game Result");
            endGameResultMenu.SetActive(true);
            this.gameManager.isPlaying = false;
        } else {
            Debug.Log($"End of Round { this.gameManager.currentRound }");
            if (this.gameManager.currentPrepareTime <= 0) {
                this.gameManager.currentPrepareTime = this.gameManager.MaxPrepareTime;
            }
            prepareMessageText.text = $"End of Round { this.gameManager.currentRound }";
            prepareRoundMenu.SetActive(true);
        }
    }

    public void StopRound() {
        Debug.Log("STOPPING ROUND");
        this.gameManager.currentRoundTime = 0f;
    }

    private IEnumerator PrepareRound() {
        prepareMessageText.text = "STARTING IN";
        prepareRoundMenu.SetActive(true);
        // player.SetActive(false);
        // ui.SetActive(false);
        Debug.Log("RoundManager.PrepareRound()");
        while ( this.gameManager.currentPrepareTime > 0 ) {
            // update countdown timer text
            SetTimerTime( this.prepareCountdownText, this.gameManager.currentPrepareTime );
            yield return new WaitForSeconds(1f);
            this.gameManager.currentPrepareTime -= 1f;
        }
        prepareRoundMenu.SetActive(false);
        StartRound();
        this.gameManager.isPlaying = true;
    }

    private void SetTimerTime(TMPro.TextMeshProUGUI timer, float time) {
        float roundedTime = Mathf.Round(time);
        timer.text = roundedTime.ToString();
    }
}
